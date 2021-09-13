using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    Rigidbody rb;

    public float offset = 4;
    public bool moveVertically;

    Vector3 startPosition;
    Vector3 endPosition;

    bool toggle;

    public float speed;
    float t;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        t = 0;
        if (moveVertically)
        {
            startPosition = new Vector3(
                rb.position.y,
                offset,
                rb.position.z);
            endPosition = new Vector3(
                rb.position.y,
                -offset,
                rb.position.z);
        }
        else
        {
            startPosition = new Vector3(offset, rb.position.y, rb.position.z);
            endPosition = new Vector3(-offset, rb.position.y, rb.position.z);
        }
    }

    void FixedUpdate()
    {
        t += Time.fixedDeltaTime * speed;

        if (toggle)
        {
            rb.MovePosition(Vector3.Lerp(endPosition, startPosition, t));

            if (t >= 1)
            {
                t = 0;
                toggle = false;
            }
        }
        else
        {
            rb.MovePosition(Vector3.Lerp(startPosition, endPosition, t));

            if (t >= 1)
            {
                t = 0;
                toggle = true;
            }
        }

    }
}
