﻿using System.Collections.Generic;
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


public class FSM {
	private Stack<FSMState> stateStack = new Stack<FSMState> ();

	public FSMState Peek()
    {
		return stateStack.Peek();
    }

	public void Update (GameObject gameObject) {
		if (stateStack.Peek() != null)
			stateStack.Peek().DoUpdate(this, gameObject);
	}

	public void pushState(FSMState state) {
		stateStack.Push (state);
	}

	public void popState() {
		stateStack.Pop ();
	}
}