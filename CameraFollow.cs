using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // this is the player or whatever object the camera needs to follow
    public Transform target;
    // how far off from the target the camera should sit
    public Vector3 offset;
    // how smooth the camera moves when it follows
    public float smoothSpeed = 0.125f;
    // horizontal sensitivity for mouse movement
    public float sensitivityX = 2f;
    // vertical sensitivity for mouse movement
    public float sensitivityY = 2f;
    // lowest angle the camera can look down
    public float minY = -30f;
    // highest angle the camera can look up
    public float maxY = 60f;

    // keeping track of the vertical angle
    private float rotationY = 0f;
    // keeping track of the horizontal angle
    private float rotationX = 0f;

    void LateUpdate()
    {
        // so here we get mouse input for moving the camera
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // update camera rotation based on mouse input
        rotationX += mouseX;
        rotationY -= mouseY;
        // limit the vertical rotation so you can't flip it around weirdly
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // now we calculate the new rotation and position for the camera
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        // check if there's a wall or something in the way of the camera
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit))
        {
            // move the camera to the point it hits so it doesn't go through the wall
            desiredPosition = hit.point;
        }

        // smoothly move the camera to its desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // make sure the camera is always looking at the target
        transform.LookAt(target);
    }
}
