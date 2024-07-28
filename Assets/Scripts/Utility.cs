using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {
    private static Utility monoInstance;
    public static Quaternion RotateToPoint(Vector3 origin, Vector3 target) {
        // angle = arc-tan(opposite / adjacent)
        float deltaX = target.x - origin.x;
        float deltaY = target.y - origin.y;
        return Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg));
    }

    public static Vector3 GetMousePosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector3 GetLocalMouse() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x, pos.y, 0);
    }

    public static void FlashSprite(SpriteRenderer sr, Color color, float duration) {
        monoInstance.StartCoroutine(_FlashSprite(sr, color, duration));
    }

    private static IEnumerator _FlashSprite(SpriteRenderer sr, Color color, float duration) {
        float startTime = Time.time;
        int bufferFrames = 0;
        Color ogColor = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a);
        while (Time.time - duration < startTime && sr != null) {
            if (bufferFrames >= 10) {
                if (sr.color.Equals(color)) {
                    sr.color = ogColor;
                } else {
                    sr.color = color;
                }
            }
            bufferFrames++;
            yield return null;
        }
        if (sr != null)
            sr.color = ogColor;
    }

    void Start() {
        monoInstance = this;
    }
}
