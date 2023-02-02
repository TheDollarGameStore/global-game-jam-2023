using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    public float delay = 0f;

    private Animator animator;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        Invoke("StartAnimation", delay);
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0f, Random.Range(0, 360)));
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay - 0.01f);
    }

    void StartAnimation()
    {
        sr.color = new Color(1f, 1f, 1f, 1f);
        animator.speed = 1f;
    }
}
