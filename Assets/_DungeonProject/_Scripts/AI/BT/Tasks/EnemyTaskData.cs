using System.Collections;
using UnityEngine;

public class EnemyTaskData : MonoBehaviour
{
    public GameObject target;

    public Vector2 GetTargetPosition()
    {
        return target.transform.position;
    }
}