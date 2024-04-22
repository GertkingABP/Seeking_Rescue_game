using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioClip lowHealthSound; // ƒобавл€ем звук низкого здоровь€
    private AudioSource audioSource;

    void Start()
    {
        // —оздаем AudioSource и присваиваем ему AudioClip
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = lowHealthSound;
        audioSource.loop = true; // «ацикливаем звук
        audioSource.volume = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<LevelHealth>().levelHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else if (GetComponent<LevelHealth>().levelHealth <= 20 && GetComponent<LevelHealth>().levelHealth >= 1)
        {
            // ѕровер€ем, если звук еще не проигрываетс€, то запускаем его
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // ≈сли здоровье выше 20 или равно 0, останавливаем проигрывание звука
            audioSource.Stop();
        }
    }
}
