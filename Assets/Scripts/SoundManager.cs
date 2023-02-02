using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audioSources;

    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayNormal(AudioClip clip)
    {
        audioSources[0].PlayOneShot(clip);
    }

    public void PlayRandomized(AudioClip clip)
    {
        audioSources[1].pitch = Random.Range(0.9f, 1.1f);
        audioSources[1].PlayOneShot(clip);
    }
}
