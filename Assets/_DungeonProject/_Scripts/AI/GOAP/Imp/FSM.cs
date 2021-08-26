using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
 * Stack-based Finite State Machine.
 * Push and pop states to the FSM.
 * 
 * States should push other states onto the stack 
 * and pop themselves off.
 */
using System;

[System.Serializable]
public class FSM {
	private Stack<FSMState> stateStack = new Stack<FSMState> ();

	[SerializeField] string CurrentStateName;

	public FSMState Peek()
    {
		return stateStack.Peek();
    }

	public void Update (GameObject gameObject) {
		stateStack.Peek().DoUpdate(this, gameObject);
	}

	public void pushState(FSMState state) {
		stateStack.Push(state);
		GUIUpdate();
	}

	public void popState() {
		stateStack.Pop();
		GUIUpdate();
	}

	void GUIUpdate()
    {
#if UNITY_EDITOR
		CurrentStateName = stateStack.Count == 0 ? "No State" : Peek().StateName;
#endif
	}
}
