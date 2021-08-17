using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField, Range(3, 30)] int nVertices = 3;
    [SerializeField] float radius = 5;
    [SerializeField] float FOVAngle = 30;
    [SerializeField] string sortingLayerName = "FOV";

    Mesh mesh;
    int nTriangles { get => nVertices - 2; }

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sortingLayerName = sortingLayerName;
    }

    private void Update()
    {
        mesh.uv = GetUV();
        mesh.vertices = GetVertices();
        mesh.triangles = GetTriangles();
    }

    private Vector2[] GetUV()
    {
        return new Vector2[nVertices];
    }

    private int[] GetTriangles()
    {
        int[] triangles = new int[nTriangles * 3];

        for (int i = 0; i < nTriangles; i++)
        {
            triangles[i * 3 + 0] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        return triangles;
    }

    private Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[nVertices];

        vertices[0] = Vector2.zero;
        for (int i = 0; i < nVertices - 1; i++)
        {
            float calculatedAngle = FOVAngle - FOVAngle * 2 / (nTriangles) * i;
            vertices[i + 1] = Quaternion.Euler(0f, 0f, calculatedAngle) * Vector3.right * radius;
        }

        return vertices;
    }    
}
