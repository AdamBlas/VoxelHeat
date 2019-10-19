using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Vector3 lookAtPoint;
    
    public void Start()
    {
        lookAtPoint = new Vector3(1, 1, 1);
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position += -transform.forward;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.position += transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(lookAtPoint, transform.up, 3f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(lookAtPoint, -transform.up, 3f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(lookAtPoint, -transform.right, 3f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(lookAtPoint, transform.right, 3f);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.RotateAround(lookAtPoint, -transform.forward, 3f);
        }
        if (Input.GetKey(KeyCode.F))
        {
            transform.RotateAround(lookAtPoint, transform.forward, 3f);
        }

    }
    public void LookAtPoint()
    {
        transform.LookAt(lookAtPoint);
    }
}
