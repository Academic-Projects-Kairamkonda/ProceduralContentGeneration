using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    public int xSize = 20;
    public int zSize = 20;

    Vector3[] vertices;
    int[] triangles;

    public PerlinNose perlinNose;


    void Start()
    {
        mesh = new Mesh();

        this.GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "Terrain";

        //StartCoroutine(CreateShape());
        CreateShape();
        UpdateMesh();
    }

    private void Update()
    {
        this.GetComponent<MeshRenderer>().material.mainTexture = perlinNose.GenerateTexture();
    }

    //IEnumerator CreateShape()
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i=0,z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f)*2f;
                vertices[i] = new Vector3(x, 0, z);
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

                //yield return new WaitForSeconds(0.01f);
            }
            vert++;
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    /* Draw on scene
    private void OnDrawGizmos()
    {
        if (vertices!=null)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], .1f);
            }
        }
    }
    */
}
