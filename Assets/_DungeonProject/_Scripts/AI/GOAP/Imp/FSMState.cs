using UnityEngine;
using System.Collections;

public class FSMState 
{
	public delegate void Update (FSM fsm, GameObject gameObj);

	public string StateName { get; private set; }

	Update update;

	public void DoUpdate(FSM fsm, GameObject gameObj)
    {
		update?.Invoke(fsm, gameObj);
    }

	public FSMState(Update update, string stateName = "No Name")
    {
		this.update = update;
		this.StateName = stateName;
    }
}

