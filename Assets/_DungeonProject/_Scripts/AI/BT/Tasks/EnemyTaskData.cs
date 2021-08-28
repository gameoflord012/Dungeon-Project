using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTaskData : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetChanged;

    [field: SerializeField] public Vector3 LastTargetPosition { get; set; }
    [SerializeField] public bool EscapedTargetChecked = true;
    [SerializeField] private GameObject target;
    [field: SerializeField] public float DestinationOffset { get; private set; } = .2f;

    public GameObject Target
    {
        get => target;
        set
        {
            if (target == value) return;
            target = value;            
            OnTargetChanged?.Invoke(target);
        }
    }

    private void Start()
    {
        if (target != null)
            OnTargetChanged?.Invoke(Target);
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