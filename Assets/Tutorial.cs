using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Wobble wobbler;

    private bool hovered;

    private bool open;

    [SerializeField] private AudioClip clickSound;

    [SerializeField] private ShowHide tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        wobbler = GetComponent<Wobble>();
    }

    // Update is called once per frame
    void Update()
    {

        if (open && Input.GetMouseButtonDown(0))
        {
            open = false;
            SoundManager.instance.PlayRandomized(clickSound);
            GameManager.instance.overlay.show = false;
            tutorialText.show = false;
            hovered = false;
        }

        if (!open && Input.GetMouseButtonDown(0) && hovered)
        {
            open = true;
            SoundManager.instance.PlayRandomized(clickSound);
            wobbler.DoTheWobble();
            GameManager.instance.overlay.show = true;
            tutorialText.show = true;
        }
    }

    private void OnMouseEnter()
    {
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
    }
}
