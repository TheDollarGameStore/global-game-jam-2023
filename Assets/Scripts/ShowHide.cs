using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHide : MonoBehaviour
{
    public bool show;

    [SerializeField] private Vector3 showPos;
    [SerializeField] private Vector3 hidePos;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, show ? showPos : hidePos, 10f * Time.deltaTime);
    }
}
