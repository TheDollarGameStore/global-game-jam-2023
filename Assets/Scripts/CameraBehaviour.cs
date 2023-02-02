using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour instance = null;

    private Vector3 defaultPos;

    private float shake;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        defaultPos = transform.position;
    }

    private void FixedUpdate()
    {
        shake = Mathf.Max(0f, shake - 0.5f);
        transform.position += (Vector3) new Vector2(Random.Range(-shake, shake), Random.Range(-shake, shake));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, defaultPos, 10f * Time.deltaTime);
    }

    public void Nudge()
    {
        transform.position += (Vector3)Vector2.up * 1.5f;
    }

    public void Shake(float amount)
    {
        shake = amount;
    }

}
