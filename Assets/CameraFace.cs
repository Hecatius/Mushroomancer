using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        // Make the sprite face the camera while keeping the original orientation on the Z-axis
        var cameraDirection = Camera.main.transform.position - transform.position;
        cameraDirection.x = 0; // Lock the rotation on the y-axis if it's a 2D sprite

        transform.rotation = Quaternion.LookRotation(-cameraDirection);
    }
}

