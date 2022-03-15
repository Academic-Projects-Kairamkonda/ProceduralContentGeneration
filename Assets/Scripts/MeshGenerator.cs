using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    [SerializeField] private int xSize = 30;
    [SerializeField] private int zSize = 30;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    float minTerrainHeight;
    float maxTerrainHeight;

    private float scale=0.3f;

    public Gradient gradient;

    void Start()
    {
        mesh = new Mesh();

        this.GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "Terrain";

        //this.GetComponent<MeshRenderer>().material.color = Color.white;

        StartCoroutine(CreateShape());
        //CreateShape();
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
                float y = GetNoise(x, z);
                vertices[i] = new Vector3(x, y, z);

                if (y > maxTerrainHeight) maxTerrainHeight = y;
                if (y < minTerrainHeight) minTerrainHeight = y;
                

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

                yield return new WaitForSeconds(0.001f);
            }

            vert++;
        }

        colors = new Color[vertices.Length];

        for (int i=0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight,maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
            }
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

    public float GetNoise(int x, int z)
    {
        float y = Mathf.PerlinNoise(x * scale, z * scale) * 2f;

        return y;
    }
}
