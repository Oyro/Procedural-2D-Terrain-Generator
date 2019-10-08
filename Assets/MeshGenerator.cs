using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public float segmentWidth;          //Width (in world coordinates) of meshsegment
    public int segmentHeight;           //Height (in world coordinates) of meshsegment
    //public ColorArrayVariable colors;   //2-element array containing top and bottom color
    public bool useGradient;
    public Color topColor;
    public Color botColor;
    //public float gradientHeight;

    [HideInInspector] public int segmentResolution;

    Vector3[] vertices;
    int[] triangles;
    Color[] vertColors;


    public virtual void Generate(MeshSegment segment, float startHeight)
    {
        InitMeshData(segment, segmentResolution);    // Create new meshdata to fit resolution
    }

    void InitMeshData(MeshSegment segment, int segmentResolution)
    {
        vertices = new Vector3[segmentResolution * 2];
        triangles = new int[(vertices.Length - 2) * 3];
        vertColors = new Color[vertices.Length];

        int iterations = segmentResolution - 1;

        for (int i = 0; i < iterations; i++)
        {
            triangles[6 * i] = 2 * i;
            triangles[6 * i + 1] = 2 * i + 1;
            triangles[6 * i + 2] = 2 * i + 3;

            triangles[6 * i + 3] = 2 * i;
            triangles[6 * i + 4] = 2 * i + 3;
            triangles[6 * i + 5] = 2 * i + 2;
        }
        segment.InitMesh(vertices, triangles);
    }

    public void GenerateMesh(MeshSegment segment, float[] heightMap, bool useCollider)
    {
        Vector2[] nodes = new Vector2[segmentResolution];
        Vector2[] colliderNodes = new Vector2[segmentResolution + 2];

        Mesh mesh = segment.mesh;

        float deltaX = segmentWidth / (heightMap.Length - 1);
        float h0 = heightMap[0];
        int _iterations = vertices.Length / 2;

        for (int i = 0; i < _iterations; i++)
        {
            float x = i * deltaX;
            float h = heightMap[i] - h0;

            vertices[i * 2 + 1] = new Vector3(x, h, 0);
            vertices[i * 2] = new Vector3(x, -segmentHeight + h, 0);;
            
            if(useGradient)
            {
                vertColors[i * 2 + 1] = topColor;
                vertColors[i * 2] = botColor;
            }
            else
            {
                vertColors[i * 2 + 1] = topColor;
                vertColors[i * 2] = botColor;
            }
            nodes[i] = vertices[i * 2 + 1];
            colliderNodes[i + 2] = nodes[i];
        }

        colliderNodes[0] = vertices[(segmentResolution - 1) * 2];
        colliderNodes[1] = vertices[0];

        mesh.vertices = vertices;
        mesh.colors = vertColors;
        mesh.RecalculateBounds();
        //segment.segmentWidth = segmentWidth;
        segment.nodes = nodes;

        if(useCollider)
        {
            segment.col.enabled = true;
            segment.col.points = colliderNodes;
        }
        else
        {
            segment.col.enabled = false;;
        }
        //segment.deltaH = heightMap[heightMap.Length - 1] - heightMap[0];
    }
}
