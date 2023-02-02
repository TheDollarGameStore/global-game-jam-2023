using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float speed;

    public Vector2 direction;

    private Vector2 posOffset;

    private Vector2 originalPos;

    private float x;

    public bool startRandomized;

    private void Start()
    {
        if (!startRandomized)
        {
            x = 0;
        }
        else
        {
            x = Random.Range(0f, 10f);
        }

        originalPos = transform.localPosition;
    }
    private void Update()
    {
        posOffset = new Vector2(Mathf.Sin(x) * direction.x, Mathf.Sin(x) * direction.y);
        x += speed * Time.deltaTime;

        this.transform.localPosition = originalPos + posOffset;
    }
}
