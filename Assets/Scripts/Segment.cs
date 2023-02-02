using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SegmentColor
{
    ORANGE,
    RED,
    BLUE,
    PURPLE,
    ROTTEN
}

public class Segment : MonoBehaviour
{
    [SerializeField] private List<Sprite> segments;

    public SegmentColor color;

    [SerializeField] private SpriteRenderer sr;

    private SpriteRenderer selfSr;

    [HideInInspector] public bool matched;

    private void Awake()
    {
        selfSr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public void SetSegment(int value)
    {
        selfSr.sprite = segments[value];
    }

    public void Update()
    {
        if (matched)
        {
            sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, 1f), 10f * Time.deltaTime);
        }
    }
}
