using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;
    public Transform playerTransform;
    public Vector3 offset;
    float startY;
    float startX;

    void Awake()
    {
        cam = Camera.main;
        startY = cam.transform.position.y;
        startX = cam.transform.position.x;
    }

    void LateUpdate()
    {
        var pos = new Vector3(startX, startY, playerTransform.position.z) + offset;

        cam.transform.position = pos;
    }
}
