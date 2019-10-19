using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    new public MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        renderer.material.color = new Color(1, 1, 1, 1f);
    }
}
