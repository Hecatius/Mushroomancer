using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookat : MonoBehaviour
{
    public Transform target; // The target GameObject to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target
    public float followSpeed = 5f; // Speed of the camera's movement
    public float rotationSpeed = 100f; // Speed of camera rotation around the target

    private float currentAngle = 0f; // Current angle around the target

    void LateUpdate()
    {
        if (target == null) return;

        // Handle rotation input for Q and E keys
        if (Input.GetKey(KeyCode.Q))
        {
            currentAngle -= rotationSpeed * Time.deltaTime; // Rotate left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentAngle += rotationSpeed * Time.deltaTime; // Rotate right
        }

        // Calculate the new offset based on the current angle
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 rotatedOffset = rotation * offset;

        // Smoothly move the camera to the new position
        Vector3 desiredPosition = target.position + rotatedOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target);
    }
}
