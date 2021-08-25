using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTaskData : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetChanged;

    private GameObject target;
    public GameObject Target { 
        get => target; 
        set
        {
            target = value;
            OnTargetChanged?.Invoke(value);
        }
    }

    public Vector2 GetTargetPosition()
    {
        return Target.transform.position;
    }

    public bool HasTarget()
    {
        return Target != null;
    }
}