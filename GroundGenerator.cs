using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MeshGenerator//ScrollingSegmentGenerator
{
    public int resolution;
    public float slope;
    public float noiseFrequency1;
    public float noiseFrequency2;
    public float strength1;
    public float strength2;


    private void Awake()
    {
        segmentResolution = resolution;
    }


    public override void Generate(MeshSegment segment, Vector2 startPos)
    {
        base.Generate(segment, startPos);
        float[] h = GenerateHeightMap(startPos.x);
        GenereateMesh(segment, h, true);
    }


    public float[] GenerateHeightMap(float startXPos)
    {
        float[] h = new float[segmentResolution];
        float deltaX = segmentWidth / (segmentResolution - 1);
        for (int i = 0; i < h.Length; i++)
        {
            h[i] = strength1 * Mathf.PerlinNoise((startXPos + i * deltaX) * noiseFrequency1, 1) + strength2 * Mathf.PerlinNoise((startXPos + i * deltaX) * noiseFrequency2, 10) + slope * i * deltaX;
        }
        //deltaH = h[h.Length - 1] - h[0];
        return h;
    }

    /*
    public float width;
    public float height;
    public int segmentResolution;
    public Transform startPos;
    public float slope;
    public float noiseFrequency1;
    public float noiseFrequency2;
    public float strength1;
    public float strength2;

    Vector3[] vertices;
    int[] triangles;
    Color[] vertColors;
    

    int lastSegmentIndex;

    private void Awake()
    {
        InitMeshData(segmentResolution);

    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in pool)
        {
            item.InitMesh(vertices, triangles);
        }


        float startHeight = startPos.position.y;
        for (int i = 0; i < pool.Length; i++)
        {
            float[] h = GenerateHeightMap(i*width);
            GenereateMesh(pool[i].meshFilter, h);
            if (i == 0)
                pool[i].transform.position = startPos.position; //- new Vector3(0, h[0], 0);
            else
                pool[i].transform.position = pool[i - 1].transform.position + new Vector3(width, pool[i - 1].segmentDeltaHeight, 0);

            pool[i].width = width;
            pool[i].endHeight = h[h.Length - 1];
            startHeight = pool[i].endHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Generate(TerrainSegment segment)
    {
        float startPos = pool[firstSegmentIndex].transform.position.x + width;
        float[] h = GenerateHeightMap(startPos);
        GenereateMesh(segment.meshFilter, h);
    }

    void InitMeshData(int segmentResolution)
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
    }

    public float[] GenerateHeightMap(float startXPos)
    {
        float[] h = new float[segmentResolution];
        float deltaX = width / (segmentResolution - 1);
        for (int i = 0; i < h.Length; i++)
        {
            h[i] = strength1*Mathf.PerlinNoise((startXPos + i * deltaX) * noiseFrequency1, 1) + strength2 * Mathf.PerlinNoise((startXPos + i * deltaX) * noiseFrequency2, 10)+slope*i*deltaX;
        }

        return h;
    }

    public void GenereateMesh(MeshFilter _meshFilter, float[] heightMap)
    {
        EdgeCollider2D col = _meshFilter.GetComponent<EdgeCollider2D>();
        TerrainSegment terrainSegment = _meshFilter.GetComponent<TerrainSegment>();
        terrainSegment.nodes = new Vector2[segmentResolution];
        Mesh _mesh = _meshFilter.sharedMesh;
        //float maxHeight = 0;
        float deltaX = width / (heightMap.Length - 1);
        float h0 = heightMap[0];
        int _iterations = vertices.Length / 2;
        for (int i = 0; i < _iterations; i++)
        {
            float xPos = i * deltaX;
            float h = heightMap[i]-h0;

            //if (h > maxHeight) maxHeight = h;

            vertices[i * 2] = new Vector3(xPos, -height+h, 0);
            vertices[i * 2 + 1] = new Vector3(xPos, h, 0);
            vertColors[i * 2] = Color.black;
            vertColors[i * 2 + 1] = Color.black;
            terrainSegment.nodes[i] = vertices[i * 2 + 1];
        }
        _mesh.vertices = vertices;
        _mesh.colors = vertColors;
        _mesh.RecalculateBounds();
        terrainSegment.width = width;
        col.points = terrainSegment.nodes;
        terrainSegment.endHeight = heightMap[heightMap.Length - 1];

        terrainSegment.segmentDeltaHeight = heightMap[heightMap.Length - 1] - heightMap[0];
    }*/
}
