using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewTask : MonoBehaviour
{
    public Text legacyText; // Ссылка на текстовое поле, которое нужно изменить
    public string newText; // Заданный текст
    public float delay = 1f; // Задержка перед отключением игрового объекта

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошедший объект имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            // Устанавливаем новый текст
            legacyText.text = newText;

            // Запускаем корутину для отключения объекта с задержкой
            StartCoroutine(DisableAfterDelay());
        }
    }

    // Корутина для отключения объекта с задержкой
    private IEnumerator DisableAfterDelay()
    {
        // Ждем заданное количество секунд
        yield return new WaitForSeconds(delay);

        // Отключаем игровой объект
        gameObject.SetActive(false);
    }
}
