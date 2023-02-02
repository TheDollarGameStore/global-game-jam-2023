using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public bool show;

    private SpriteRenderer sr;

    [SerializeField] private float showAlpha;
    [SerializeField] private float hideAlpha;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.Lerp(sr.color, new Color(sr.color.r, sr.color.g, sr.color.b, show ? showAlpha : hideAlpha), 10f * Time.deltaTime);
    }
}
