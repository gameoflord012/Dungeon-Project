using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Movement Data")]
public class MovementDataSO : ScriptableObject
{
    public float maxSpeed = 5, acceleration = 5, deacceleration = 8, velocityThreshold = 6f;
}
