using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainSegment : MonoBehaviour
{
    public MeshFilter meshFilter;
    Mesh mesh;
    //public TerrainGenerator terrainGenerator;
    public float width;
    public float endHeight;
    public float segmentDeltaHeight;
    public Vector2[] nodes;

    private void Awake()
    {
        
    }
    
    private void Start()
    {
        
    }

    public void InitMesh(Vector3[] verts, int[] tris)
    {
        mesh = meshFilter.mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
    }
}
