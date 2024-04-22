using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Text pauseText;
    public Text hintText; // Новое текстовое поле для подсказки
    public GameObject player; // Ссылка на игрового персонажа
    private bool isPaused = false;

    // Получаем ссылку на скрипт FirstPersonLook
    private FirstPersonLook firstPersonLookScript;

    void Start()
    {
        // Получаем ссылку на скрипт FirstPersonLook
        firstPersonLookScript = player.GetComponentInChildren<FirstPersonLook>();

        // По умолчанию скрываем панель паузы
        SetPauseVisibility(false);
    }

    void Update()
    {
        // Проверяем нажатие клавиши Escape для постановки на паузу и возобновления игры
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Проверяем нажатие клавиши Enter для выхода в меню
        if (isPaused && Input.GetKeyDown(KeyCode.Return))
        {
            LoadScene();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
            SetPauseVisibility(true);
            pauseText.text = "Пауза";
            hintText.text = "\n\n\n\t\t\tВозобновить игру: Esc\t\t\t\n\n\t\t\tВыйти в главное меню: Enter\t\t\t";
        }
        else
        {
            SetPauseVisibility(false);
            pauseText.text = "";
            hintText.text = "";
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем время в игре
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор
        Cursor.visible = true; // Показываем курсор

        // Отключаем вращение камеры игрового персонажа
        if (firstPersonLookScript != null)
            firstPersonLookScript.enabled = false;

        // Отключаем ввод при постановке на паузу
        DisableInput();
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время в игре
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор
        Cursor.visible = false; // Скрываем курсор

        // Включаем вращение камеры игрового персонажа
        if (firstPersonLookScript != null)
            firstPersonLookScript.enabled = true;

        // Включаем ввод при возобновлении игры
        EnableInput();
    }

    // Метод для загрузки другой сцены
    public void LoadScene()
    {
        Time.timeScale = 1f; // Убеждаемся, что время возобновлено перед загрузкой новой сцены
        SceneManager.LoadScene("menu");
    }

    // Отключение ввода
    void DisableInput()
    {
        // Блокируем все компоненты управления, кроме PauseManager
        foreach (var component in FindObjectsOfType<MonoBehaviour>())
        {
            if (component != this && component.GetType() != typeof(PauseManager))
                component.enabled = false;
        }
    }

    // Включение ввода
    void EnableInput()
    {
        // Разблокируем все компоненты управления, кроме PauseManager
        foreach (var component in FindObjectsOfType<MonoBehaviour>())
        {
            if (component.GetType() != typeof(PauseManager))
                component.enabled = true;
        }
    }

    // Устанавливаем видимость паузы и текста
    void SetPauseVisibility(bool isVisible)
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(isVisible);
        }
        else
        {
            Debug.LogError("Pause Panel is not assigned!");
        }

        if (pauseText != null)
        {
            pauseText.enabled = isVisible;
        }
        else
        {
            Debug.LogError("Pause Text is not assigned!");
        }

        if (hintText != null)
        {
            hintText.enabled = isVisible;
        }
        else
        {
            Debug.LogError("Hint Text is not assigned!");
        }
    }


    public FirstPersonAudio firstPersonAudio; // Объявление поля для компонента FirstPersonAudio

    // Добавлен метод для управления состоянием паузы из других скриптов
    public void SetPaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f; // Приостановка времени
            pauseText.text = "Пауза"; // Установка текста на экране паузы
            // Дополнительные действия при входе в паузу
            // Например, вызов приостановки звуков движения
            if (firstPersonAudio != null)
                firstPersonAudio.SetPaused(true); // Вызов метода, который приостанавливает звуки движения
        }
        else
        {
            Time.timeScale = 1f; // Возобновление времени
            pauseText.text = ""; // Очистка текста на экране паузы
            // Дополнительные действия при выходе из паузы
            // Например, вызов возобновления звуков движения
            if (firstPersonAudio != null)
                firstPersonAudio.SetPaused(false); // Вызов метода, который возобновляет звуки движения
        }
    }
}
