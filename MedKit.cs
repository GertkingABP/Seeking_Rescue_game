using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class MedKit : MonoBehaviour
{
    BoxCollider medCollider;
    public AudioClip myClip;
    private AudioSource mySource;
    public float volume = 0.5f;
    public Text txtToDisplay; // Текст для отображения подсказки
    private bool playerInTriggerZone = false;
    private Collider playerCollider; // Добавлено поле для хранения коллайдера игрока
    private bool isPickedUp = false; // Флаг для проверки, была ли аптечка уже подобрана

    void Start()
    {
        mySource = GetComponent<AudioSource>();
        medCollider = GetComponent<BoxCollider>();
        medCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            playerCollider = otherCollider; // Сохраняем коллайдер игрока
            txtToDisplay.gameObject.SetActive(true);
            txtToDisplay.text = "Нажмите E, чтобы подобрать аптечку";
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = false;
            txtToDisplay.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInTriggerZone && !isPickedUp)
        {
            // Проверяем, нажата ли кнопка E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Используем сохраненный коллайдер игрока
                if (playerCollider != null)
                {
                    LevelHealth playerHealth = playerCollider.gameObject.GetComponent<LevelHealth>();

                    // Добавляем проверку текущего здоровья игрока
                    if (playerHealth != null && playerHealth.levelHealth < 100)
                    {
                        PickUpMedKit(playerHealth);
                    }
                }
            }
        }
    }

    void PickUpMedKit(LevelHealth playerHealth)
    {
        int HPAmount = Random.Range(10, 50);

        if ((playerHealth.levelHealth + HPAmount) < 100)
        {
            playerHealth.levelHealth += HPAmount;
        }

        if ((playerHealth.levelHealth + HPAmount) >= 100)
        {
            playerHealth.levelHealth = 100;
        }

        txtToDisplay.text = "Подобрана аптечка";

        mySource.PlayOneShot(myClip, volume);

        isPickedUp = true; // Устанавливаем флаг, что аптечка подобрана

        // Скрываем объект
        SetObjectVisibility(false);

        // Запускаем задержку перед окончательным отключением
        StartCoroutine(DeactivateWithDelay());
    }

    void SetObjectVisibility(bool isVisible)
    {
        // Получаем рендерер объекта
        Renderer renderer = GetComponent<Renderer>();

        // Если у объекта есть рендерер, изменяем его видимость
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }

    IEnumerator DeactivateWithDelay()
    {
        // Добавляем задержку в 1.5 секунды
        yield return new WaitForSeconds(0.7f);

        txtToDisplay.gameObject.SetActive(false);
        // Выключаем объект
        gameObject.SetActive(false);
    }
}
