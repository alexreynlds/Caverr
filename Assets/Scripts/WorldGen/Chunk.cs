using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    int vertexIndex = 0;
    List<Vector3> verticies = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    bool [,,] voxelMap = new bool[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];

    void Start()
    {
        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();
    }

    void PopulateVoxelMap(){
        for( int y = 0; y < VoxelData.ChunkHeight; y++ ){
            for( int x = 0; x < VoxelData.ChunkWidth; x++ ){  
                for( int z = 0; z < VoxelData.ChunkWidth; z++ ){  
                    voxelMap[x, y, z] = true;
                }
            }
        }
    }

    void CreateMeshData(){
        for( int y = 0; y < VoxelData.ChunkHeight; y++ ){
            for( int x = 0; x < VoxelData.ChunkWidth; x++ ){  
                for( int z = 0; z < VoxelData.ChunkWidth; z++ ){  
                    AddVoxelData(new Vector3(x, y, z));
                }
            }
        }
    }

    bool CheckVoxel(Vector3 pos){
        int x = Mathf.FloorToInt (pos.x);
        int y = Mathf.FloorToInt (pos.y);
        int z = Mathf.FloorToInt (pos.z);

        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 || z < 0 || z > VoxelData.ChunkWidth - 1){
            return false;
        }

        return voxelMap[x, y, z];
    }

    void CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    void AddVoxelData( Vector3 pos ) {
        for (int j = 0; j < 6; j++){
            if(!CheckVoxel(pos + VoxelData.faceChecks[j])){
                verticies.Add (pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 0]]);
                verticies.Add (pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 1]]);
                verticies.Add (pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 2]]);
                verticies.Add (pos + VoxelData.voxelVerts[VoxelData.voxelTris[j, 3]]);
                uvs.Add (VoxelData.voxelUvs[0]);
                uvs.Add (VoxelData.voxelUvs[1]);
                uvs.Add (VoxelData.voxelUvs[2]);
                uvs.Add (VoxelData.voxelUvs[3]);
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);
                vertexIndex += 4;
            }
        }
    }
}
