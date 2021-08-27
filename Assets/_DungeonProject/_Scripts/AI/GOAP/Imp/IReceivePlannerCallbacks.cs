using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public interface IReceivePlannerCallbacks
{
	void planFailed(IGoalStateProvider goalStateProvider);
	void planFound(IGoalStateProvider goalStateProvider, Queue<IGoapAction> actions);
	void actionFinished(IGoapAction finishedAction);
	void actionBegin(IGoapAction beginningAction);
	void planAborted(IGoapAction aborter);
}