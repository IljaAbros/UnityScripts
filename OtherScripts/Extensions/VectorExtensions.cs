using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions {
    public static float MagnitudeInDirection(Vector3 vector, Vector3 direction, bool normalizeParameters = true) {
        if (normalizeParameters) direction.Normalize();
        return Vector3.Dot(vector, direction);
    }
	
    public static Vector3 Abs(this Vector3 vector) {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
	
    public static bool IsParallel(Vector3 direction, Vector3 otherDirection, float precision = .0001f) {
        return Vector3.Cross(direction, otherDirection).sqrMagnitude < precision;
    }
	
    public static bool IsInDirection(Vector3 direction, Vector3 otherDirection) {
        return Vector3.Dot(direction, otherDirection) > 0f;
    }
	
    public static float AngleTo(this Vector2 a, Vector2 b) {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * 180 / Mathf.PI;
    }

    public static Vector3 Snap(this Vector3 vector, float stepX, float stepY, float stepZ) {
        float x = vector.x;
        float y = vector.y;
        float z = vector.z;

        if (stepX > 0) { x = x.Snap(stepX); }
        if (stepY > 0) { y = y.Snap(stepY); }
        if (stepZ > 0) { z = z.Snap(stepZ); }

        return new Vector3(x, y, z);
    }
}
