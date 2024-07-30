using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
    private Transform player;
    private Camera cam;

    public float zoomSensitivity = 1.0f;

    void Start() {
        player = GameObject.Find("Player").transform;
        cam = GetComponent<Camera>();
    }

    void Update() {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        // if (cam.orthographicSize >= 1.0f && cam.orthographicSize <= 10.0f)
        //     cam.orthographicSize += Input.mouseScrollDelta.y * zoomSensitivity;
        // // For overscrolling (also will throw error when too low)
        // cam.orthographicSize = Mathf.Min(10.0f, cam.orthographicSize);
        // cam.orthographicSize = Mathf.Max(1.0f, cam.orthographicSize);
    }
}
