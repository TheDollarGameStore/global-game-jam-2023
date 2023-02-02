using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private List<GameObject> segmentPrefabs;

    [HideInInspector] public List<Segment> segments;

    private int pieceSize;

    // Start is called before the first frame update
    void Awake()
    {
        Generate();
    }

    void Generate()
    {
        segments = new List<Segment>();
        pieceSize = Random.Range(1, 4);

        for (int i = 0; i < pieceSize; i++)
        {
            Segment newSegment = Instantiate(segmentPrefabs[Random.Range(0, segmentPrefabs.Count)], transform.position + (Vector3)(Vector2.down * (i * 16f)), Quaternion.identity, transform).GetComponent<Segment>();

            if (pieceSize == 1)
            {
                newSegment.SetSegment(3);
            }
            else if (i == 0)
            {
                newSegment.SetSegment(0);
            }
            else if (i < pieceSize - 1)
            {
                newSegment.SetSegment(1);
            }
            else
            {
                newSegment.SetSegment(2);
            }
            
            segments.Add(newSegment);
        }
    }
}
