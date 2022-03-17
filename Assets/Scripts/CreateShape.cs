using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APG_CW1
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class CreateShape : MonoBehaviour
    {
        Mesh mesh;

        Vector3[] vertices;
        int[] triangles;

        void Start()
        {
            mesh = new Mesh();
            this.GetComponent<MeshFilter>().mesh = mesh;
            this.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));

            GetEquilateralTriangle();
            UpdateShape();
        }

        void GetSquarePosition()
        {
            vertices = new Vector3[]
            {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1)
            };


            triangles = new int[]
            {
            0, 1, 2,
            1, 3, 2
            };

        }


        void GetEquilateralTriangle()
        {
            vertices = new Vector3[]
            {
            new Vector3(0,0,0),
            new Vector3(0.5f,0,1f),
            new Vector3(1,0,0),
            new Vector3 (0.5f,0,-1f)
            };

            triangles = new int[]
            {
            0, 1, 2,
            2, 3, 0
            };

        }

        void UpdateShape()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}