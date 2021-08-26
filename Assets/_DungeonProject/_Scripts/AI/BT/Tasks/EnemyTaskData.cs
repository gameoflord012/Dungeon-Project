using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTaskData : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetChanged;
    public Vector3 LastTargetPosition { get; private set; }

    [SerializeField] private GameObject target;
    public GameObject Target
    {
        get => target;
        set
        {
            target = value;

            if(target != null)
                LastTargetPosition = target.transform.position;
            OnTargetChanged?.Invoke(target);
        }
    }

    private void Awake()
    {
        target = FindObjectOfType<PlayerInputHandler>().gameObject;
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