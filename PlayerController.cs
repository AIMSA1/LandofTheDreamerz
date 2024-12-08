using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // how fast the player moves
    public float moveSpeed = 5f;
    // how high the player jumps
    public float jumpForce = 5f;
    // public float dodgeSpeed = 10f; // (this was for dodging, but it's turned off rn)
    

    private Rigidbody rb; // physics for the player
    // private bool canDodge = true; // dodging is commented out for now

    private bool isMovementEnabled = true; // can the player move?

    void Start()
    {
        // set up the physics
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // if movement is disabled, don't do anything
        if (!isMovementEnabled) return;

        // get input for moving left-right and forward-back
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;

        // only move the player if there's input
        if (movement.magnitude > 0f)
        {
            // get the direction the camera is looking in
            Quaternion cameraRotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            Vector3 moveDirection = cameraRotation * movement;

            // check if the player can move in that direction
            if (CanMove(moveDirection))
            {
                // make the player face the direction they're moving
                transform.rotation = Quaternion.LookRotation(moveDirection);

                // move the player forward
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
            }
        }

        // make the player jump when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        
    }

    

    private bool IsGrounded()
    {
        // check if there's ground below the player
        float rayDistance = 1.1f; // adjust this if the player gets stuck
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }

    private bool CanMove(Vector3 direction)
    {
        // see if there's something blocking the way
        float detectionDistance = 0.6f; // adjust this based on player size
        return !Physics.Raycast(transform.position, direction, detectionDistance);
    }

    // lets you turn player movement on or off
    public void SetMovementEnabled(bool isEnabled)
    {
        isMovementEnabled = isEnabled;
    }
}
