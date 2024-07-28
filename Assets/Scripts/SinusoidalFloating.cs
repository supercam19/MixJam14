using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SinusoidalFloating : MonoBehaviour
{
    private float startY;
    private int frames;
    [SerializeField] private float amplitude = 1;
    [SerializeField] private float frequency = 1;
    [SerializeField] private float offset = 0;

    void Update() {
        transform.position = new Vector3(transform.position.x, transform.parent.position.y + offset + amplitude * Mathf.Sin(frames / frequency),
            transform.position.z);
        frames++;
    }
}
