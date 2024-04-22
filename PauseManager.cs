using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public Text pauseText;
    public Text hintText; // ����� ��������� ���� ��� ���������
    public GameObject player; // ������ �� �������� ���������
    private bool isPaused = false;

    // �������� ������ �� ������ FirstPersonLook
    private FirstPersonLook firstPersonLookScript;

    void Start()
    {
        // �������� ������ �� ������ FirstPersonLook
        firstPersonLookScript = player.GetComponentInChildren<FirstPersonLook>();

        // �� ��������� �������� ������ �����
        SetPauseVisibility(false);
    }

    void Update()
    {
        // ��������� ������� ������� Escape ��� ���������� �� ����� � ������������� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // ��������� ������� ������� Enter ��� ������ � ����
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
            pauseText.text = "�����";
            hintText.text = "\n\n\n\t\t\t����������� ����: Esc\t\t\t\n\n\t\t\t����� � ������� ����: Enter\t\t\t";
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
        Time.timeScale = 0f; // ������������� ����� � ����
        Cursor.lockState = CursorLockMode.None; // ������������ ������
        Cursor.visible = true; // ���������� ������

        // ��������� �������� ������ �������� ���������
        if (firstPersonLookScript != null)
            firstPersonLookScript.enabled = false;

        // ��������� ���� ��� ���������� �� �����
        DisableInput();
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ ����� � ����
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������
        Cursor.visible = false; // �������� ������

        // �������� �������� ������ �������� ���������
        if (firstPersonLookScript != null)
            firstPersonLookScript.enabled = true;

        // �������� ���� ��� ������������� ����
        EnableInput();
    }

    // ����� ��� �������� ������ �����
    public void LoadScene()
    {
        Time.timeScale = 1f; // ����������, ��� ����� ������������ ����� ��������� ����� �����
        SceneManager.LoadScene("menu");
    }

    // ���������� �����
    void DisableInput()
    {
        // ��������� ��� ���������� ����������, ����� PauseManager
        foreach (var component in FindObjectsOfType<MonoBehaviour>())
        {
            if (component != this && component.GetType() != typeof(PauseManager))
                component.enabled = false;
        }
    }

    // ��������� �����
    void EnableInput()
    {
        // ������������ ��� ���������� ����������, ����� PauseManager
        foreach (var component in FindObjectsOfType<MonoBehaviour>())
        {
            if (component.GetType() != typeof(PauseManager))
                component.enabled = true;
        }
    }

    // ������������� ��������� ����� � ������
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


    public FirstPersonAudio firstPersonAudio; // ���������� ���� ��� ���������� FirstPersonAudio

    // �������� ����� ��� ���������� ���������� ����� �� ������ ��������
    public void SetPaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f; // ������������ �������
            pauseText.text = "�����"; // ��������� ������ �� ������ �����
            // �������������� �������� ��� ����� � �����
            // ��������, ����� ������������ ������ ��������
            if (firstPersonAudio != null)
                firstPersonAudio.SetPaused(true); // ����� ������, ������� ���������������� ����� ��������
        }
        else
        {
            Time.timeScale = 1f; // ������������� �������
            pauseText.text = ""; // ������� ������ �� ������ �����
            // �������������� �������� ��� ������ �� �����
            // ��������, ����� ������������� ������ ��������
            if (firstPersonAudio != null)
                firstPersonAudio.SetPaused(false); // ����� ������, ������� ������������ ����� ��������
        }
    }
}
