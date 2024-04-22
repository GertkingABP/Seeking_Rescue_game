using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TaskDisplay : MonoBehaviour
{
    public Text taskText;
    private bool isTabPressed = false;
    private Coroutine displayCoroutine;
    private float displayDuration = 3f; // ����� ����������� ������ ��� ������� �� Tab

    private void Start()
    {
        taskText.enabled = false; // �������� � �������, ������� �������
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
        // ��� ������������� ���� ���������, ���� �� ������ ������� Tab ����� ������
        if (!isTabPressed)
        {
            // ���� ������� Tab �� ���� ������, �������� ����� ������ ����� displayDuration �������
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
