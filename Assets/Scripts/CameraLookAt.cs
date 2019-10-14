using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Vector3 lookAtPoint;
    public float distance = 5;

    public void Start()
    {
        lookAtPoint = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            distance++;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            distance--;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(lookAtPoint, -transform.up, 3f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(lookAtPoint, transform.up, 3f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(lookAtPoint, transform.right, 3f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(lookAtPoint, -transform.right, 3f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.RotateAround(lookAtPoint, transform.forward, 3f);
        }
        if (Input.GetKey(KeyCode.F))
        {
            transform.RotateAround(lookAtPoint, -transform.forward, 3f);
        }

    }
    public void LookAtPoint()
    {
        transform.LookAt(lookAtPoint);
    }
}
