using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageEnemy1 : MonoBehaviour
{
    public AudioClip myClip;
    public AudioClip myClip_ache;
    private AudioSource mySource;
    public float volume = 0.5f;

    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider myCollider)
    {
        if (myCollider.CompareTag("Player"))
        {
            int damageAmount = Random.Range(10, 21);
            myCollider.GetComponent<LevelHealth>().levelHealth -= damageAmount;

            StartCoroutine(PlaySoundsWithDelay());
        }
    }

    IEnumerator PlaySoundsWithDelay()
    {
        // Проигрываем первый звук
        mySource.PlayOneShot(myClip, volume);

        // Добавляем задержку
        yield return new WaitForSeconds(0.2f);

        // Проигрываем второй звук
        mySource.PlayOneShot(myClip_ache, volume);
    }
}
