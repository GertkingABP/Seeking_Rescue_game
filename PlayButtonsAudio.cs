using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonsAudio : MonoBehaviour
{
    public AudioSource audioSrc;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        audioSrc.Play();
    }
}