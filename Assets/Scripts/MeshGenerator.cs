using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    [SerializeField] private int xSize = 50;
    [SerializeField] private int zSize = 50;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    [SerializeField] private float amplitude1=1;
    [SerializeField] private float frequency1 =1;
    [SerializeField] private float amplitude2=0.5f;
    [SerializeField] private float frequency2 =1f;
    [SerializeField] private float amplitude3=0.1f;
    [SerializeField] private float frequency3=5f;

    public Gradient gradient;

    void Start()
    {
        mesh = new Mesh();

        this.GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "Terrain";


        StartCoroutine(CreateShape());
    }

    private void Update()
    {
        UpdateMesh();
    }

    IEnumerator CreateShape()
    //void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i=0,z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = CalculateNoise(x, z);
                vertices[i] = new Vector3(x, y, z);

                i++;
            }
        }

        triangles = new int[xSize*zSize*6];

        int vert = 0;
        int tris = 0;


        for (int z = 0; z < zSize; z++)
        {
            for (int i = 0; i < xSize; i++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1; 
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                yield return new WaitForSeconds(0.0001f);
            }

            vert++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

    private float CalculateNoise(float x, float z)
    {
        float noise;
        noise = Mathf.PerlinNoise(x, z) * 5;
        noise += Mathf.PerlinNoise(x * amplitude1, z * amplitude1) * frequency1;
        noise -= Mathf.PerlinNoise(x * amplitude2, z * amplitude2) * frequency2;
        noise += Mathf.PerlinNoise(x * amplitude3, z * amplitude3) * frequency3 * 2;
        return noise;
    }
}
