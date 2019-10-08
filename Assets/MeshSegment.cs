using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshSegment : MonoBehaviour
{
    public MeshFilter meshFilter;
    public PolygonCollider2D col;

    //[HideInInspector] public float segmentWidth; //Remove
    //[HideInInspector] public float deltaH;      //Remove
    [HideInInspector] public Vector2[] nodes;      // TODO: Remove?


    [HideInInspector] public Mesh mesh;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh = new Mesh();
    }
    public void InitMesh(Vector3[] verts, int[] tris) // TODO: Remove?
    {
        mesh.vertices = verts;
        mesh.triangles = tris;
    }
    public float GetEndHeight()
    {
        return nodes[nodes.Length-1].y;
    }
    public virtual void Generate()
    {
        Debug.Log("Generating meshsegment");
    }
}
