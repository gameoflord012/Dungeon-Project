using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;

public sealed class GoapAgent : MonoBehaviour {

	private FSM stateMachine;

	private FSMState idleState; // finds something to do
	private FSMState moveToState; // moves to a target
	private FSMState performActionState; // performs an action
	
	private HashSet<IGoapAction> availableActions;
	private Queue<IGoapAction> currentActions;
	private HashSet<IGoapAction> finishedActions;

	private IGoap dataProvider; // this is the implementing class that provides our world data and listens to feedback on planning

	private GoapPlanner planner;

#if UNITY_EDITOR
	public string GetCurrentFSMName()
    {
		Assert.IsNotNull(stateMachine.Peek());
		return stateMachine.Peek().StateName;
    }

	public IEnumerable<IGoapAction> GetCurrentActions()
    {
		return currentActions;
    }

	public IEnumerable<IGoapAction> GetFinisedAction()
    {
		return finishedActions;
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
			finishedActions.Clear();

			// GOAP planning

			// get the world state and the goal we want to plan for
			HashSet<KeyValuePair<string,object>> worldState = dataProvider.getWorldState();
			HashSet<KeyValuePair<string,object>> goal = dataProvider.createGoalState();

			// Plan
			Queue<IGoapAction> plan = planner.plan(gameObject, availableActions, worldState, goal);
			if (plan != null) {
				// we have a plan, hooray!
				currentActions = plan;
				dataProvider.planFound(goal, plan);

				fsm.popState(); // move to PerformAction state
				fsm.pushState(performActionState);

			} else {
				// ugh, we couldn't get a plan
				Debug.Log("<color=orange>Failed Plan:</color>"+prettyPrint(goal));
				dataProvider.planFailed(goal);
				fsm.popState (); // move back to IdleAction state
				fsm.pushState (idleState);
			}
		},

		"IdleState");
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
			if ( dataProvider.moveAgent(action) ) {
				fsm.popState();
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
			dataProvider.actionsFinished();
			return;
		}

		IGoapAction action = currentActions.Peek();
		if (action.isDone()) {
			// the action is done. Remove it so we can perform the next one
			finishedActions.Add(currentActions.Dequeue());
			}

			if (hasActionPlan()) {
				// perform the next action
				action = currentActions.Peek();

				if (action.isInRange()) {
					// we are in range, so perform the action
					bool success = action.perform(gameObj);

					if (!success) {
						// action failed, we need to plan again
						fsm.popState();
						fsm.pushState(idleState);
						dataProvider.planAborted(action);
					}
				} else {
					// we need to move there first
					// push moveTo state
					fsm.pushState(moveToState);
				}

			} else {
				// no actions left, move to Plan state
				fsm.popState();
				fsm.pushState(idleState);
				dataProvider.actionsFinished();
			}

		},
		"PerformState");
	}

	private void findDataProvider() {
		foreach (Component comp in gameObject.GetComponents(typeof(Component)) ) {
			if ( typeof(IGoap).IsAssignableFrom(comp.GetType()) ) {
				dataProvider = (IGoap)comp;
				return;
			}
		}
	}

	private void loadActions ()
	{
		IGoapAction[] actions = gameObject.GetComponents<IGoapAction>();
		foreach (IGoapAction a in actions) {
			availableActions.Add (a);
		}
		Debug.Log("Found actions: "+prettyPrint(actions));
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
