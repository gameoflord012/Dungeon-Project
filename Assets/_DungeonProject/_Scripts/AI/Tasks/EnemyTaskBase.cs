using System.Collections;
using UnityEngine;

public class EnemyTaskBase : MonoBehaviour
{
    protected ActorInputEvents inputEvents;
    protected ActorMovement movement;
    protected Damager damager;
    protected EnemyTaskData data;
    protected FOV fov;
    protected AIPathControl pathControl;

    private void Awake()
    {
        damager = GetComponentInParent<Damager>();
        movement = GetComponentInParent<ActorMovement>();
        data = GetComponentInParent<EnemyTaskData>();
        inputEvents = GetComponentInParent<ActorInputEvents>();
        fov = movement.GetComponentInChildren<FOV>();
        pathControl = GetComponentInParent<AIPathControl>();
    }
}
