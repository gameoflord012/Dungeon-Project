using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;
using System.Linq;

public sealed class GoapAgent : MonoBehaviour {

	[SerializeField] GoapMono[] goaps;

	[SerializeField, HideInInspector]
	private FSM stateMachine;

	private FSMState idleState; // finds something to do
	private FSMState moveToState; // moves to a target
	private FSMState performActionState; // performs an action
	
	private HashSet<IGoapAction> availableActions;
	private Queue<IEnumerator<PerformState>> currentEnumerators;	
	private Queue<IGoapAction> currentActions;	
	private HashSet<IGoapAction> finishedActions;
	
	private HashSet<KeyValuePair<string, object>> worldState;	
	private HashSet<KeyValuePair<string, object>> goalState;

	private IGoap currentGoap; // this is the implementing class that provides our world data and listens to feedback on planning

	private GoapPlanner planner;

#if UNITY_EDITOR
	public string GetCurrentFSMName()
    {
		Assert.IsNotNull(stateMachine.Peek());
		return stateMachine.Peek().StateName;		
    }

	public string GetCurrentGoapName()
    {
		if (currentGoap == null) return "Not available";
		return currentGoap.GetType().Name;
    }

	public IEnumerable<IGoapAction> GetCurrentActions()
    {
		return currentActions;
    }

	public IEnumerable<IGoapAction> GetFinisedAction()
    {
		return finishedActions;
    }

	public IEnumerable<KeyValuePair<string, object>> GetWorldStates()
    {
		if (currentGoap == null)
			return Enumerable.Empty<KeyValuePair<string, object>>();

		return worldState;
    }

	public IEnumerable<KeyValuePair<string, object>> GetGoalStates()
	{
		if (currentGoap == null)
			return Enumerable.Empty<KeyValuePair<string, object>>();

		return goalState;
	}	
#endif

	void Start () {
		stateMachine = new FSM ();
		availableActions = new HashSet<IGoapAction> ();
		currentActions = new Queue<IGoapAction> ();
		planner = new GoapPlanner ();
		finishedActions = new HashSet<IGoapAction>();
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


	public void addAction(IGoapAction a) {
		availableActions.Add (a);
	}

	public IGoapAction getAction(Type action) {
		foreach (IGoapAction g in availableActions) {
			if (g.GetType().Equals(action) )
			    return g;
		}
		return null;
	}

	public void removeAction(IGoapAction action) {
		availableActions.Remove (action);
	}

	private bool hasActionPlan() {
		return currentActions.Count > 0;
	}

	private void createIdleState() {
		idleState = new FSMState((fsm, gameObj) => {
			currentGoap = null;

			foreach(IGoap goap in goaps)
            {
				worldState = goap.getWorldState();
				goalState = goap.createGoalState();

				Queue<IGoapAction> plan = planner.plan(gameObject, availableActions, worldState, goalState);
				if (plan != null)
				{
					finishedActions.Clear();
					currentActions = plan;
					currentEnumerators = GetIEnumerator(plan, gameObj);
					goap.planFound(goalState, plan);

					fsm.popState();
					fsm.pushState(performActionState);

					currentGoap = goap;
					break;

				}
				else
				{
					Debug.Log("<color=orange>Failed Plan:</color>" + prettyPrint(goalState));
					goap.planFailed(goalState);
				}
			}
		},

		"IdleState");
	}

    private Queue<IEnumerator<PerformState>> GetIEnumerator(Queue<IGoapAction> actions, GameObject gameObj)
    {
		Queue<IEnumerator<PerformState>> result = new Queue<IEnumerator<PerformState>>();
		foreach (IGoapAction action in actions) result.Enqueue(action.perform(gameObj));
		return result;
    }

    private void createMoveToState() {
		moveToState = new FSMState((fsm, gameObj) => {
			// move the game object

			IGoapAction action = currentActions.Peek();
			//if (action.requiresInRange() && action.Target == null) {
			//	Debug.Log("<color=red>Fatal error:</color> Action requires a target but has none. Planning failed. You did not assign the target in your Action.checkProceduralPrecondition()");
			//	fsm.popState(); // move
			//	fsm.popState(); // perform
			//	fsm.pushState(idleState);
			//	return;
			//}

			// get the agent to move itself
			if ( currentGoap.moveAgent(action) ) {
				fsm.popState();
			}
			else
            {
				fsm.popState(); // move
				fsm.popState(); // perform
				fsm.pushState(idleState);
				currentGoap.planAborted(action);
			}

			/*MovableComponent movable = (MovableComponent) gameObj.GetComponent(typeof(MovableComponent));
			if (movable == null) {
				Debug.Log("<color=red>Fatal error:</color> Trying to move an Agent that doesn't have a MovableComponent. Please give it one.");
				fsm.popState(); // move
				fsm.popState(); // perform
				fsm.pushState(idleState);
				return;
			}

			float step = movable.moveSpeed * Time.deltaTime;
			gameObj.transform.position = Vector3.MoveTowards(gameObj.transform.position, action.target.transform.position, step);

			if (gameObj.transform.position.Equals(action.target.transform.position) ) {
				// we are at the target location, we are done
				action.setInRange(true);
				fsm.popState();
			}*/
		},
		"MoveToState");
	}
	
	private void createPerformActionState() {

		performActionState = new FSMState((fsm, gameObj) => {
			// perform the action
			if (!hasActionPlan()) {
				// no actions to perform
				Debug.Log("<color=red>Done actions</color>");
				fsm.popState();
				fsm.pushState(idleState);
				currentGoap.actionsFinished();
				return;
			}

			if (hasActionPlan()) {
				IGoapAction action = currentActions.Peek();
				IEnumerator<PerformState> enumerator = currentEnumerators.Peek();

				if (action.isInRange()) {
					if (enumerator.MoveNext())
					{
						if (enumerator.Current == PerformState.falied)
						{
							fsm.popState();
							fsm.pushState(idleState);
							currentGoap.planAborted(action);
						}						
					}
					else
                    {
						currentEnumerators.Dequeue();
						finishedActions.Add(currentActions.Dequeue());
					}		
				} else {
					fsm.pushState(moveToState);
				}

			} else {
				// no actions left, move to Plan state
				fsm.popState();
				fsm.pushState(idleState);
				currentGoap.actionsFinished();
			}

		},
		"PerformState");
	}

	private void findDataProvider() {
		//foreach (Component comp in gameObject.GetComponents(typeof(Component)) ) {
		//	if ( typeof(IGoap).IsAssignableFrom(comp.GetType()) ) {
		//		currentGoap = (IGoap)comp;
		//		return;
		//	}
		//}
	}

	private void loadActions ()
	{
		IGoapAction[] actions = gameObject.GetComponents<IGoapAction>();
		foreach (IGoapAction a in actions) {
			availableActions.Add (a);
		}
		//Debug.Log("Found actions: "+prettyPrint(actions));
	}

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
}
