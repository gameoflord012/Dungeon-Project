using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTaskData : MonoBehaviour
{
    public UnityEvent<GameObject> OnTargetChanged;

    [SerializeField] private GameObject target;
    public GameObject Target
    {
        get => target;
        set
        {
            target = value;
            OnTargetChanged?.Invoke(value);
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