using UnityEngine;

public class Utility : MonoBehaviour
{
    public static Quaternion RotateToPoint(Vector3 origin, Vector3 target) {
        // angle = arc-tan(opposite / adjacent)
        float deltaX = target.x - origin.x;
        float deltaY = target.y - origin.y;
        return Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg));
    }
}
