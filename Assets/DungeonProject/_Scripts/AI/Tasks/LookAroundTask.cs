using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundTask : EnemyTaskBase
{
    [Task]
    public void LookAround(int angle)
    {
        Vector2 pointerDirection = fov.transform.rotation * Quaternion.Euler(0, 0, angle) * Vector2.right;
        inputEvents.OnPointerPositionChangedCallback((Vector2)transform.position + pointerDirection);
        Task.current.Succeed();

        if(Task.isInspected)
        {
            Task.current.debugInfo = "Look direction: " + pointerDirection;
        }
    }
}
