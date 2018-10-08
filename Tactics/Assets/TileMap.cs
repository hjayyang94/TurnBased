using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class TileMap : MonoBehaviour {
    
    //Size of tile map
    int size_x = 50;
    int size_z = 50;

    float tileSize = 1.0f;

	void Start () {
        BuildMesh();
	}


    void BuildMesh()
    {
        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;

        //However wide + 1 = number of Vertices horizontal.
        int vSize_x = size_x + 1;
        int vSize_z = size_z + 1;
        int numVerts = vSize_x * vSize_z;

        //Generating mesh data

        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] norms = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];

        int x, z;

        for ( z = 0; z < vSize_z; z++)
        {
            for ( x = 0; x < vSize_x; x++)
            {
                vertices[ z * vSize_x + x ] = new Vector3( x * tileSize, 0, z * tileSize );
                norms[ z * vSize_x + x ] = Vector3.up;
                uv[z * vSize_x + x ] = new Vector2( (float) x / size_x, (float) z / size_z );
            }
        }
        

        for ( z = 0; z < size_z; z++ )
        {
            for ( x = 0; x < size_x; x++)
            {
                
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;

        

                triangles[triOffset + 0] = z * vSize_x + x + 0;
                triangles[triOffset + 1] = z * vSize_x + x + 1;
                triangles[triOffset + 2] = z * vSize_x + vSize_x + x;


                triangles[triOffset + 3] = z * vSize_x + x + 1;
                triangles[triOffset + 4] = z * vSize_x + vSize_x + x + 0;
                triangles[triOffset + 5] = z * vSize_x + vSize_x + x + 1;

            }
        }
        

        //Initialize and populate mesh with data;
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = norms;
        mesh.uv = uv;
        

        //Assign mesh to filter/renderer/collider
        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        MeshCollider mesh_collider = GetComponent<MeshCollider>();

        mesh_filter.mesh = mesh;
    }
	
	
}
