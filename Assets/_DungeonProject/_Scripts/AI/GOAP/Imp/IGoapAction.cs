
using UnityEngine;
using System.Collections.Generic;

public interface IGoapAction {
	IEnumerable<KeyValuePair<string, object>> GetPreconditions();
	IEnumerable<KeyValuePair<string, object>> GetEffects();

	/* The cost of performing the action. 
	 * Figure out a weight that suits the action. 
	 * Changing it will affect what actions are chosen during planning.*/
	float Cost { get; set; }

	/**
	 * Reset any variables that need to be reset before planning happens again.
	 */
	void reset();

	/**
	 * Is the action done?
	 */
	bool isDone();

	/**
	 * Procedurally check if this action can run. Not all actions
	 * will need this, but some might.
	 */
	bool checkProceduralPrecondition(GameObject agent);

	/**
	 * Run the action.
	 * Returns True if the action performed successfully or false
	 * if something happened and it can no longer perform. In this case
	 * the action queue should clear out and the goal cannot be reached.
	 */
	bool perform(GameObject agent);

	/**
	 * Does this action need to be within range of a target game object?
	 * If not then the moveTo state will not need to run for this action.
	 */
	//bool requiresInRange ();

	/**
	 * Are we in range of the target?
	 * The MoveTo state will set this and it gets reset each time this action is performed.
	 */
	bool isInRange();
}