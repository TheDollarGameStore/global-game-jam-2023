using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SegmentColor
{
    ORANGE,
    RED,
    BLUE,
    PURPLE
}

public class Segment : MonoBehaviour
{
    [SerializeField] private List<Sprite> segments;

    public SegmentColor color;

    private SpriteRenderer sr;

    [HideInInspector] public bool matched;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public void SetSegment(int value)
    {
        sr.sprite = segments[value];
    }

    public void Update()
    {
        if (matched)
        {
            sr.color = Color.Lerp(sr.color, Color.black, 5f * Time.deltaTime);
        }
    }
}
