using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskDisplay : MonoBehaviour
{
    public Text taskText;
    private bool isTabPressed = false;
    private Coroutine displayCoroutine;
    private float displayDuration = 3f; // Время отображения текста при нажатии на Tab

    private void Start()
    {
        taskText.enabled = false; // Начинаем с текстом, который невидим
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTabPressed = true;
            DisplayTask();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            isTabPressed = false;
            if (displayCoroutine != null)
                StopCoroutine(displayCoroutine);
            displayCoroutine = StartCoroutine(HideAfterDelay());
        }
    }

    private void OnEnable()
    {
        // При возобновлении игры проверяем, была ли нажата клавиша Tab перед паузой
        if (!isTabPressed)
        {
            // Если клавиша Tab не была нажата, скрываем текст задачи через displayDuration времени
            if (displayCoroutine != null)
                StopCoroutine(displayCoroutine);
            displayCoroutine = StartCoroutine(HideAfterDelay());
        }
    }

    private void DisplayTask()
    {
        taskText.enabled = true;
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (!isTabPressed)
            taskText.enabled = false;
    }
}
