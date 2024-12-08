using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float speed = 3f; // Speed of movement
    public float moveDistance = 5f; // Maximum distance from the starting point

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;

    void Start()
    {
        // Store the starting position
        startPosition = transform.position;

        // Calculate the initial target position
        targetPosition = startPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        // Move the block toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the block reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Reverse direction
            movingUp = !movingUp;

            // Update the target position
            targetPosition = movingUp
                ? startPosition + Vector3.up * moveDistance
                : startPosition - Vector3.up * moveDistance;
        }
    }
}
