using Panda;
using System.Collections;
using UnityEngine;

public class StopTask : EnemyTaskBase
{
    [Task]
    public void StopMoving()
    {
        pathControl.CancelPathFinding();
        movement.ResetMovement();
        Task.current.Succeed();
    }

    [Task]
    public void ResetTarget()
    {
        data.target = null;
        Task.current.Succeed();
    }
}