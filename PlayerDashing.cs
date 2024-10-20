using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody playerRigidBody;
    private PlayerMovement playerMovement;
    private Stamina stamina; 

    [Header("Dashing")]
    public float dashForce = 50;
    public float dashUpwardForce;
    public float maxDashYSpeed = 15;
    public float dashDuration = 0.25f;

    [Header("Settings")]
    public bool useCameraForward = false;
    public bool allowAllDirections = true;
    public bool disableGravity = true;
    public bool resetVel = true;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.E;

    private int maxDashCount = 3;
    private int currentDashCount;
    private bool canDash = true;

    private Vector3 delayedForceToApply;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        stamina = GetComponent<Stamina>();
        currentDashCount = maxDashCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash && currentDashCount > 0)
        {
           if (stamina != null && stamina.currentStamina >= 1f)
            {
                Dash();
            }
           else if (stamina.currentStamina < 1f)
            {
                Debug.Log("Staminayok");
            }
        }
    }

    private void Dash()
    {
        playerMovement.dashing = true;
        playerMovement.maxYSpeed = maxDashYSpeed;

        Transform forwardT;

        if (useCameraForward)
        {
            forwardT = playerCam;
        }
        else
        {
            forwardT = orientation;
        }

        Vector3 direction = GetDirection(forwardT);
        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
        {
            playerRigidBody.useGravity = false;
        }

        delayedForceToApply = new Vector3(forceToApply.x, 0f, forceToApply.z);
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);

        currentDashCount--;
        canDash = false;
        if (stamina != null)
        {
            stamina.UseStamina();
        }
    }

    private void DelayedDashForce()
    {
        if (resetVel)
        {
            playerRigidBody.velocity = Vector3.zero;
        }

        playerRigidBody.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        playerMovement.dashing = false;
        playerMovement.maxYSpeed = 0;

        if (disableGravity)
        {
            playerRigidBody.useGravity = true;
        }

        Invoke(nameof(EnableDash), dashDuration); 
    }

    private void EnableDash()
    {
        canDash = true;
        currentDashCount = maxDashCount; 
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
        {
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        }
        else
        {
            direction = forwardT.forward;
        }

        if (verticalInput == 0 && horizontalInput == 0)
        {
            direction = forwardT.forward;
        }

        return direction.normalized;
    }
}