using UnityEngine;

public static class BaseCalculations
{
    public static float Distance(Vector3 a, Vector3 b) =>
        (a - b).sqrMagnitude;

    public static bool IsInRange(Vector3 a, Vector3 b, float range) =>
        (a - b).sqrMagnitude <= range;
}