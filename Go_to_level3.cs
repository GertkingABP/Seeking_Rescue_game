using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class Go_to_level3 : MonoBehaviour
{
    BoxCollider mCollider;
    public Text txtToDisplay; // Текст для отображения подсказки
    private bool playerInTriggerZone = false;
    private Collider playerCollider; // Добавлено поле для хранения коллайдера игрока

    void Start()
    {
        mCollider = GetComponent<BoxCollider>();
        mCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            playerCollider = otherCollider; // Сохраняем коллайдер игрока
            txtToDisplay.gameObject.SetActive(true);
            txtToDisplay.text = "Уровень пройден";
        }
    }

    void Update()
    {
        if (playerInTriggerZone)
        {
            // Используем сохраненный коллайдер игрока
            if (playerCollider != null)
            {
                PickUpM();
            }
        }
    }

    void PickUpM()
    {
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
        // Добавляем задержку
        yield return new WaitForSeconds(1.0f);

        txtToDisplay.gameObject.SetActive(false);
        // Выключаем объект
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene3");
    }
}