using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APG_CW1
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] private int width = 256;
        [SerializeField] private int height = 256;

        [SerializeField] private int depth = 20;
        [SerializeField] private float scale = 20f;

        [SerializeField] private float offsetX = 100f;
        [SerializeField] private float offsetY = 100f;

        Terrain terrain;

        private void Start()
        {
            RandomOffset();
            terrain = this.GetComponent<Terrain>();

        }

        private void Update()
        {
            terrain.terrainData = GenerateTerrain(terrain.terrainData);

            // To move the terrain on each frame
            //offsetX += Time.deltaTime; 
        }

        public TerrainData GenerateTerrain(TerrainData terrainData)
        {
            terrainData.heightmapResolution = width + 1;

            terrainData.size = new Vector3(width, depth, height);

            terrainData.SetHeights(0, 0, GenerateHeights());

            return terrainData;

        }

        private float[,] GenerateHeights()
        {
            float[,] heights = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    heights[x, y] = CalculateHeight(x, y);
                }
            }

            return heights;
        }

        private float CalculateHeight(int x, int y)
        {
            float xCoord = (float)x / width * scale + offsetX;
            float yCoord = (float)y / height * scale + offsetY;

            return Mathf.PerlinNoise(xCoord, yCoord);

        }

        public void RandomOffset()
        {
            offsetX = UnityEngine.Random.Range(0, 9999f);
            offsetY = UnityEngine.Random.Range(0, 9999f);
        }
    }
}