using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[ExecuteInEditMode]
public class TerrainGenerator : MeshGenerator
{
    public int splits;
    public float strength;
    public float decay;

    public int initialSplits;

    private void Awake()
    {
        segmentResolution = (int)Mathf.Pow(2, splits)+1;
    }

    public override void Generate(MeshSegment segment, Vector2 startPos)
    {
        segmentResolution = (int)Mathf.Pow(2, splits) + 1;
        base.Generate(segment, startPos);
        float[] h = GenerateHeightMap(startPos.y);
        GenereateMesh(segment, h, false);
    }

    public float[] GenerateHeightMap(float startYPos)
    {
        float tempStrength = strength;
        int endIndex = segmentResolution-1;
        float[] h = new float[endIndex + 1];
        
       
        h[0] = 0;
        float offset = startTransform.localPosition.y - startYPos;
        h[endIndex] = Random.Range(-tempStrength, 0) + offset;

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
        return Random.Range(-strength, 0);
    }
}
