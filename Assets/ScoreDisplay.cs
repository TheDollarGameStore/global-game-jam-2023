using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float delay;

    private bool counting;

    [HideInInspector] public int score;

    private int displayScore;

    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", lifeSpan);
        Invoke("StartCounting", delay);
    }

    void StartCounting()
    {
        counting = true;
    }

    private void FixedUpdate()
    {
        if (counting && displayScore < score)
        {
            displayScore += 1;
            scoreText.text = "$" + displayScore.ToString();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
