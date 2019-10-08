using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshScroller : MonoBehaviour
{
    public TerrainGenerator terrainGenerator;
    public MeshSegment[] pool;
    public Transform startPos;
    public Transform segmentContainer;


    // Start is called before the first frame update
    void Start()
    {
        pool = new MeshSegment[segmentContainer.childCount];
        for (int i = 0; i < segmentContainer.childCount; i++)
        {
            pool[i] = segmentContainer.GetChild(i).GetComponent<MeshSegment>();
        }
        for (int i = 0; i < pool.Length; i++)
        {
            float h0 = 0;
            if(i==0)
            {
                h0 = terrainGenerator.GetRandomStartHeight();
            }
            else
            {
                h0 = pool[i - 1].GetEndHeight();    
            }
            terrainGenerator.Generate(pool[i], h0);
            pool[i].transform.position = startPos.position + new Vector3(i*terrainGenerator.segmentWidth, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
