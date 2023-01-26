using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public MeshFilter meshFilter;

    void Start()
    {
        int vertexIndex = 0;

        List<Vector3> verticies = new List<Vector3>();

        List<int> triangles = new List<int>();

        List<Vector2> uvs = new List<Vector2>();

        for (int j = 0; j < 6; j++){
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = VoxelData.voxelTris[j, i];
                verticies.Add(VoxelData.voxelVerts[triangleIndex]);
                triangles.Add (vertexIndex);

                uvs.Add(VoxelData.voxelUvs[i]);

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
