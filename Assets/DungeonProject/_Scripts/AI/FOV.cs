using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField, Range(3, 100)] int nVertices = 3;
    [SerializeField] float viewDistance = 5;
    [SerializeField] float FOVAngle = 30;
    [SerializeField] string sortingLayerName = "FOV";
    [SerializeField] LayerMask obstacleLayerMask;

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
        mesh.vertices = GetVertices();
        mesh.uv = GetUV();
        mesh.triangles = GetTriangles();
    }

    private Vector2[] GetUV()
    {
        Vector2[] uv = new Vector2[nVertices];
        for (int i = 0; i < nVertices; i++) uv[i] = new Vector2(.5f, .5f);
        return uv;
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
            vertices[i + 1] = RaycastAtAngle(FOVAngle - FOVAngle * 2 / (nTriangles) * i, viewDistance);

        return vertices;
    }

    private Vector2 RaycastAtAngle(float calculatedAngle, float raycastDisntance)
    {
        Vector2 LocalRaycastDirection = Quaternion.Euler(0f, 0f, calculatedAngle) * Vector3.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(LocalRaycastDirection), raycastDisntance, obstacleLayerMask);
        return LocalRaycastDirection * (hit.collider ? hit.distance : raycastDisntance);        
    }
}
