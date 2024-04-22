using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioClip lowHealthSound; // ��������� ���� ������� ��������
    private AudioSource audioSource;

    void Start()
    {
        // ������� AudioSource � ����������� ��� AudioClip
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = lowHealthSound;
        audioSource.loop = true; // ����������� ����
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
            // ���������, ���� ���� ��� �� �������������, �� ��������� ���
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // ���� �������� ���� 20 ��� ����� 0, ������������� ������������ �����
            audioSource.Stop();
        }
    }
}
