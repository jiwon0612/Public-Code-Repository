using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUV : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        //Physics2D.Raycast;
    }

}
