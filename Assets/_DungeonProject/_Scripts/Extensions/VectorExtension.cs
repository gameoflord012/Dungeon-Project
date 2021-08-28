using System.Collections;
using UnityEngine;

public static class VectorExtension
{
    public static bool LengthSmalllerThan(this Vector2 vector2, float distance)
    {
        return vector2.sqrMagnitude < distance * distance;
    }

    public static bool LengthSmalllerThan(this Vector3 vector3, float distance)
    {
        return vector3.sqrMagnitude < distance * distance;
    }
}