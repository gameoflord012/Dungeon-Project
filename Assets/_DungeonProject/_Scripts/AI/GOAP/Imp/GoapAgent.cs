using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;
using System.Linq;

public sealed class GoapAgent : MonoBehaviour {

	[SerializeField] GoapMono[] goaps;
	[SerializeField] private FSM stateMachine;

	private IWorldStateProvider[] worldStateProviders;
	private IGoalStateProvider[] goalStateProviders;
	private IReceivePlannerCallbacks[] plannerCallbackReceiver;
	private GoapPlanner planner;

	private FSMState idleState;
	private FSMState moveToState;
	private FSMState performActionState;
	
	private HashSet<IGoapAction> availableActions;
	private Queue<IEnumerator<PerformState>> currentEnumerators;
	private Queue<IGoapAction> currentActions;	
	private HashSet<IGoapAction> finishedActions;
	
	private HashSet<KeyValuePair<string, object>> worldState;
	private HashSet<KeyValuePair<string, object>> currentGoalState;
	private IGoapAgent currentGoapAgent;

    public HashSet<KeyValuePair<string, object>> WorldState { 
		get => worldState;
		set
		{
			worldState = value;
#if UNITY_EDITOR
			GUI.changed = true;
#endif
		}
	}
    public HashSet<KeyValuePair<string, object>> CurrentGoalState { 
		get => currentGoalState;
		set
		{
			currentGoalState = value;
#if UNITY_EDITOR
			GUI.changed = true;
#endif
		}
	}
    public IGoapAgent CurrentGoapAgent { 
		get => currentGoapAgent; 
		set { 
			currentGoapAgent = value;
#if UNITY_EDITOR
			GUI.changed = true;
#endif
		} 
	}
    public Queue<IGoapAction> CurrentActions { 
		get => currentActions;
		set
		{
			currentActions = value;
#if UNITY_EDITOR
			GUI.changed = true;
#endif
		}
	}
    public HashSet<IGoapAction> FinishedActions { 
		get => finishedActions;
		set { 
			finishedActions = value;
#if UNITY_EDITOR
			GUI.changed = true;
#endif
		}
	}

#if UNITY_EDITOR
    public string GetCurrentGoapName()
    {
		if (CurrentGoapAgent == null) return "Not available";
		return CurrentGoapAgent.GetType().Name;
    }
	public IEnumerable<IGoapAction> GetCurrentActions()
    {
		return CurrentActions;
    }
	public IEnumerable<IGoapAction> GetFinisedAction()
    {
		return FinishedActions;
    }
	public IEnumerable<KeyValuePair<string, object>> GetWorldState()
    {		
		return Enumerable.Empty<KeyValuePair<string, object>>();
    }
	public IEnumerable<KeyValuePair<string, object>> GetGoalState()
	{
		if (hasActionPlan()) return CurrentGoalState;
		return Enumerable.Empty<KeyValuePair<string, object>>();		
	}	
#endif

	void Start () {		
		stateMachine = new FSM ();
		availableActions = new HashSet<IGoapAction> ();
		CurrentActions = new Queue<IGoapAction> ();
		planner = new GoapPlanner ();
		FinishedActions = new HashSet<IGoapAction>();
		findDataProvider ();
		createIdleState ();
		createMoveToState ();
		createPerformActionState ();
		stateMachine.pushState (idleState);
		loadActions ();
	}
	
	void Update () {
		stateMachine.Update (this.gameObject);
	}

	private void createIdleState() {
		idleState = new FSMState((fsm, gameObj) => {

			WorldState = GetProvidersWorldState();

			foreach (IGoalStateProvider goalStateProvider in goalStateProviders)
            {
                CurrentGoalState = new HashSet<KeyValuePair<string, object>>(goalStateProvider.GetGoalState());

				Queue<IGoapAction> plan = planner.plan(gameObject, availableActions, WorldState, CurrentGoalState);
				if (plan != null)
				{
					FinishedActions = new HashSet<IGoapAction>();
					CurrentActions = plan;
					currentEnumerators = GetIEnumerator(plan, gameObj);
					plannerCallbackReceiver.ForEach(x => x.planFound(goalStateProvider, plan));

					fsm.popState();
					fsm.pushState(performActionState);
					break;

				}
				else
				{
					Debug.Log("<color=orange>Failed Plan:</color>" + prettyPrint(CurrentGoalState));
					plannerCallbackReceiver.ForEach(x => x.planFailed(goalStateProvider));
				}
			}
		},

		"IdleState");
	}
    private void createMoveToState() {
		moveToState = new FSMState((fsm, gameObj) => {

			IGoapAction action = CurrentActions.Peek();
			if ( CurrentGoapAgent.moveAgent(action) ) {
				fsm.popState();
			}
			else
            {
				fsm.popState(); // move
				fsm.popState(); // perform
				fsm.pushState(idleState);
				plannerCallbackReceiver.ForEach(x => x.planAborted(action));
			}
		},
		"MoveToState");
	}
	private void createPerformActionState() {

		performActionState = new FSMState((fsm, gameObj) => {

			if (!hasActionPlan()) {
				Debug.Log("<color=red>Done actions</color>");
				fsm.popState();
				fsm.pushState(idleState);				
				return;
			}

			if (hasActionPlan()) {
				IGoapAction action = CurrentActions.Peek();
				IEnumerator<PerformState> enumerator = currentEnumerators.Peek();

				if (action.isInRange()) {
					if (enumerator.MoveNext())
					{
						if (enumerator.Current == PerformState.falied)
						{
							fsm.popState();
							fsm.pushState(idleState);
							//CurrentGoap.planAborted(action);
							plannerCallbackReceiver.ForEach(x => x.planAborted(action));
						}						
					}
					else
                    {
						currentEnumerators.Dequeue();
						FinishedActions.Add(CurrentActions.Dequeue());
#if UNITY_EDITOR
						GUI.changed = true;
#endif
					}		
				} else {
					fsm.pushState(moveToState);
				}

			} else {
				fsm.popState();
				fsm.pushState(idleState);
				plannerCallbackReceiver.ForEach(x => x.actionsFinished());
			}

		},
		"PerformState");
	}

	private bool hasActionPlan()
	{
		return CurrentActions.Count > 0;
	}
	private void findDataProvider() {
		worldStateProviders = GetComponentsInChildren<IWorldStateProvider>();
		goalStateProviders = GetComponentsInChildren<IGoalStateProvider>();
		plannerCallbackReceiver = GetComponentsInParent<IReceivePlannerCallbacks>();
		CurrentGoapAgent = GetComponentInParent<IGoapAgent>();

	}
	private HashSet<KeyValuePair<string, object>> GetProvidersWorldState()
	{
		HashSet<KeyValuePair<string, object>> result = new HashSet<KeyValuePair<string, object>>();

		foreach (IWorldStateProvider worldStateProvider in worldStateProviders)
			result.UnionWith(worldStateProvider.GetWorldState());

		return result;
	}
	private Queue<IEnumerator<PerformState>> GetIEnumerator(Queue<IGoapAction> actions, GameObject gameObj)
	{
		Queue<IEnumerator<PerformState>> result = new Queue<IEnumerator<PerformState>>();
		foreach (IGoapAction action in actions) result.Enqueue(action.perform(gameObj));
		return result;
	}
	private void loadActions ()
	{
		IGoapAction[] actions = gameObject.GetComponents<IGoapAction>();
		foreach (IGoapAction a in actions) availableActions.Add(a);
		
	}

    #region Prints
    public static string prettyPrint(HashSet<KeyValuePair<string,object>> state) {
		String s = "";
		foreach (KeyValuePair<string,object> kvp in state) {
			s += kvp.Key + ":" + kvp.Value.ToString();
			s += ", ";
		}
		return s;
	}
	public static string prettyPrint(Queue<IGoapAction> actions) {
		String s = "";
		foreach (IGoapAction a in actions) {
			s += a.GetType().Name;
			s += "-> ";
		}
		s += "GOAL";
		return s;
	}
	public static string prettyPrint(IGoapAction[] actions) {
		String s = "";
		foreach (IGoapAction a in actions) {
			s += a.GetType().Name;
			s += ", ";
		}
		return s;
	}
	public static string prettyPrint(IGoapAction action) {
		String s = ""+action.GetType().Name;
		return s;
	}
    #endregion
}
