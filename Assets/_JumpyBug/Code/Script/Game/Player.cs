using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRb;

    public Vector3 jumpDirection;
    public bool jumping;
    public bool grounded;

    public float jumpPowerChangeSpeed = 1;
    public float mediumJumpThreshold = 0.9f;
    public MeshRenderer meshRenderer;

    public float jumpPowerRounded;
    public Platform currentPlatform;

    float jumpPower;
    bool powerToggle;
    Vector3 startPosition;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();

        startPosition = playerRb.position;
        grounded = false;
        currentPlatform = null;
    }

    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        if (jumpPower < mediumJumpThreshold)
        {
            jumpPowerRounded = 0.75f;
            meshRenderer.sharedMaterial.color = Color.yellow;
        }
        else
        {
            jumpPowerRounded = 1;
            meshRenderer.sharedMaterial.color = Color.green;
        }

        if (!grounded)
        {
            RaycastHit hit;
            var hitSomething = Physics.Raycast(playerRb.position, -Vector3.up, out hit, 0.25f);

            if (hitSomething)
            {
                var plat = hit.transform.GetComponent<Platform>();
                if (plat != currentPlatform)
                {
                    currentPlatform = plat;
                    transform.SetParent(plat.t.transform);
                    playerRb.isKinematic = true;
                    grounded = true;
                }
            }
        }

        if (jumping)
        {
            if (jumpPower > 1)
                jumpPower = 1;
            else
                jumpPower += Time.deltaTime * jumpPowerChangeSpeed;
        }
#if !UNITY_EDITOR && UNITY_ANDROID
        if (Input.touchCount > 0)
#endif
        {
#if UNITY_EDITOR
            if (Input.GetButtonDown("Jump"))
#elif UNITY_ANDROID
            if (Input.GetTouch(0).phase == TouchPhase.Began)
#endif
            {
                jumping = true;
            }
#if UNITY_EDITOR
            if (Input.GetButtonUp("Jump") && grounded)
#elif UNITY_ANDROID
            if (Input.GetTouch(0).phase == TouchPhase.Ended && grounded)
#endif
            {
                grounded = false;
                playerRb.isKinematic = false;

                playerRb.AddForce(jumpDirection * jumpPowerRounded, ForceMode.Impulse);
                jumpPower = 0;
                jumping = false;
            }
        }
        if (playerRb.position.y < -10)
            ResetPosition();
    }

    void ResetPosition()
    {
        playerRb.position = startPosition + (Vector3.up * 5);
        playerRb.velocity = Vector3.zero;

        playerRb.isKinematic = false;

        grounded = false;
        jumping = false;
        currentPlatform = null;
    }
}
