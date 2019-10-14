using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    public Transform frontCamera, topCamera, sideCamera;
    public CameraLookAt mainCamera;

    public void UpdateCameras(float x, float y, float z)
    {
        frontCamera.position = new Vector3(x, y, -100);
        topCamera.position = new Vector3(x, 100, z);
        sideCamera.position = new Vector3(-100, y, z);

        mainCamera.lookAtPoint = new Vector3(x, y, z);
        mainCamera.LookAtPoint();
    }
}
