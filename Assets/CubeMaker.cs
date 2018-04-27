using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class CubeMaker : MonoBehaviour
{

    public Vector3 size = Vector3.one;
    MeshCreator mc = new MeshCreator();
    public float FACTOR = 8f;
    public float bump = 2f;

    // Update is called once per frame
    void Update()
    {

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        // one submesh for each face
        Vector3 center = new Vector3(0, 0, 0);

        mc.Clear(); // Clear internal lists and mesh

        for (int row = 0; row < 50; row++)
        {
            for (int col = 0; col < 50; col++)
            {
                center.Set(col * size.x , 0, row * size.z );
                CreateCube(center);
            }
        }

        meshFilter.mesh = mc.CreateMesh();

    }

    void CreateCube(Vector3 center)
    {
        Vector3 cubeSize = size * 0.5f;

        // top of the cube
        // t0 is top left point

        float x1 = center.x + cubeSize.x;
        float x2 = center.x - cubeSize.x;
        float x3 = center.x - cubeSize.x;
        float x4 = center.x + cubeSize.x;

        float z1 = center.z - cubeSize.z;
        float z2 = center.z - cubeSize.z;
        float z3 = center.z + cubeSize.z;
        float z4 = center.z + cubeSize.z;

        float height1 = center.y + cubeSize.y + bump * Perlin.Noise(x1 / FACTOR, z1/ FACTOR);
        float height2 = center.y + cubeSize.y + bump * Perlin.Noise(x2 / FACTOR, z2 / FACTOR);
        float height3 = center.y + cubeSize.y + bump * Perlin.Noise(x3 / FACTOR, z3 / FACTOR);
        float height4 = center.y + cubeSize.y + bump * Perlin.Noise(x4 / FACTOR, z4 / FACTOR);

        Vector3 t0 = new Vector3(x1, height1 , z1);
        Vector3 t1 = new Vector3(x2, height2 , z2);
        Vector3 t2 = new Vector3(x3, height3 , z3);
        Vector3 t3 = new Vector3(x4, height4 , z4);

        // bottom of the cube
        //Vector3 b0 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        //Vector3 b1 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z - cubeSize.z);
        //Vector3 b2 = new Vector3(center.x - cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);
        //Vector3 b3 = new Vector3(center.x + cubeSize.x, center.y - cubeSize.y, center.z + cubeSize.z);

        // Top square
        mc.BuildTriangle(t0, t1, t2);
        mc.BuildTriangle(t0, t2, t3);

        //// Bottom square
        //mc.BuildTriangle(b2, b1, b0);
        //mc.BuildTriangle(b3, b2, b0);

        //// Back square
        //mc.BuildTriangle(b0, t1, t0);
        //mc.BuildTriangle(b0, b1, t1);

        //mc.BuildTriangle(b1, t2, t1);
        //mc.BuildTriangle(b1, b2, t2);

        //mc.BuildTriangle(b2, t3, t2);
        //mc.BuildTriangle(b2, b3, t3);

        //mc.BuildTriangle(b3, t0, t3);
        //mc.BuildTriangle(b3, b0, t0);
    }
}