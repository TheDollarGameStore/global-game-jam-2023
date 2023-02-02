using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    bool started;

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, -200f), 10f * Time.deltaTime);
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            Destroy(GetComponent<Bob>());
        } 
    }
}
