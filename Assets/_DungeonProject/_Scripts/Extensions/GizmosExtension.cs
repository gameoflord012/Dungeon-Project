using UnityEngine;

public static class DrawArrow
{
    public static void ForGizmo(Vector3 position, Vector3 destination, float arrowHeadLength = 0.5f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawLine(position, destination);

        Vector3 right = Quaternion.LookRotation(Vector3.forward, destination - position) * Quaternion.AngleAxis(180 + arrowHeadAngle, Vector3.forward) * Vector3.up;
        Vector3 left = Quaternion.LookRotation(Vector3.forward, destination - position) * Quaternion.AngleAxis(180 - arrowHeadAngle, Vector3.forward) * Vector3.up;

        Vector3 endpoint = (destination + position) / 2f;
        Gizmos.DrawRay(endpoint, right * arrowHeadLength);
        Gizmos.DrawRay(endpoint, left * arrowHeadLength);
    }
}