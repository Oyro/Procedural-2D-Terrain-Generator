using UnityEngine;

//[ExecuteInEditMode]
public class TerrainGenerator : MeshGenerator
{
    public int splits;          // Number of times each segmentline will be split
    public float startStrength;
    public float decay;         // The strength-decay after each split

    public int initialSplits;   // Number of splits before decay takes place
    public bool useCollider;

    private void Awake()
    {
        segmentResolution = (int)Mathf.Pow(2, splits)+1;    // Number of horizontal points
    }

    public override void Generate(MeshSegment segment, float startHeight)
    {
        // Generates a new segment, connecting it the one before at startPos
        segmentResolution = (int)Mathf.Pow(2, splits) + 1;
        base.Generate(segment, startHeight);
        float[] h = GenerateHeightMap(startHeight);
        GenerateMesh(segment, h, useCollider);
    }

    public float[] GenerateHeightMap(float startYPos)
    {
        // Generates the heightmap, starting at startYPos
        float tempStrength = startStrength;
        int endIndex = segmentResolution-1;
        float[] h = new float[endIndex + 1];
        Debug.Log(startYPos);
        h[0] = startYPos;
        h[endIndex] = Random.Range(-tempStrength, 0);

        int delta;
        for (int i = 1; i <= endIndex / 2; i *= 2)
        {
            if( !(i/2<initialSplits) )
            {
                tempStrength /= (decay * i);
            }
            
            delta = endIndex / i;
            for (int j = 0; j < i; j++)
            {
                int index = endIndex / (2 * i) + j * delta;
                h[index] = (h[index - delta / 2] + h[index + delta / 2]) / 2;
                h[index] += Random.Range(-tempStrength, 0);
            }
        }
        return h;
    }
    public float GetRandomStartHeight()
    {
        return Random.Range(-startStrength, 0);
    }
}
