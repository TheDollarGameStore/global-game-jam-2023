using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : MonoBehaviour
{
    public float speed;

    public float twistAngle;

    private float angleOffset;

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

    }

    private void Update()
    {
        angleOffset = Mathf.Sin(x) * twistAngle;
        x += speed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angleOffset));

    }
}
