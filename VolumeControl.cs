using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 0.25f;
    
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}