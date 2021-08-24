using Panda;
using System.Collections;
using UnityEngine;

public class StopTask : EnemyTaskBase
{
    [Task]
    public void StopMoving()
    {
        pathControl.CancelPathFinding();
        movement.StopMoving();
        Task.current.Succeed();
    }    
}