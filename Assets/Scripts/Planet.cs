using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APG_CW1
{
    public class Planet : MonoBehaviour
    {
        [Range(2, 256)]
        public int resolution = 10;

        const int m_value = 6;

        [SerializeField, HideInInspector]
        MeshFilter[] meshFilters;
        TerrainFace[] terrainFaces;

        private void OnValidate()
        {
            Intialize();
            GenerateMesh();
        }

        void Intialize()
        {
            if (meshFilters == null || meshFilters.Length == 0)
            {
                meshFilters = new MeshFilter[m_value];
            }
            terrainFaces = new TerrainFace[m_value];

            Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < m_value; i++)
            {
                if (meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;

                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }

                terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
            }
        }

        void GenerateMesh()
        {
            foreach (TerrainFace face in terrainFaces)
            {
                face.ConstructMesh();
            }
        }
    }
}