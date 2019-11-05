using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fpsText;

    void Update()
    {
        fpsText.text = "FPS: " + Mathf.RoundToInt(1.0f / Time.deltaTime);
    }
}
