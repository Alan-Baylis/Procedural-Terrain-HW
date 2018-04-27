using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour {
    public Terrain terrain;
    [Range(0,1)]
    public float bumpiness= 0.2f;
    public float range = 50f;
	// Use this for initialization
	void Start () {
        
        //GetComponent<Terrain>().terrainData = td;

    }
	
	// Update is called once per frame
	void Update () {
        
        TerrainData td = terrain.terrainData;
        float[,] heights = td.GetHeights(0, 0, 500, 500);
        for (int x = 0; x < 500; x++)
        {
            for (int y = 0; y < 500; y++)
            {
                heights[x, y] =((float)Perlin.Noise(x / range, y / range)* bumpiness);
            }
        }
        td.SetHeights(0, 0, heights);
    }
}
