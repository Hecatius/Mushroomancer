using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookat : MonoBehaviour
{
    public Transform target; // The target GameObject to follow
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the target
    public float followSpeed = 5f; // Speed of the camera's movement

    void LateUpdate()
    {
        if (target == null) return;

        // Smoothly move the camera to the target position with the offset
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target);
    }
}
