using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCameraManager : MonoBehaviour
{
    public Camera frontCam, sideCam, topCam;

    public void ToggleFrontCam()
    {
        frontCam.enabled = !frontCam.enabled;
    }
    public void ToggleSideCam()
    {
        sideCam.enabled = !sideCam.enabled;
    }
    public void ToggleTopCam()
    {
        topCam.enabled = !topCam.enabled;
    }


}
