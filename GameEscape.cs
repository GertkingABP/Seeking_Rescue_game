using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEscape : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            //Application.Quit ();
            SceneManager.LoadScene("menu");
            
            //��� ��������(�� ������������)
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnEnable()
    {
        // ���������� ������ ��� �������� ����� �����
        Cursor.visible = true;
        // ������������ ������ ��� �������� ����� �����
        Cursor.lockState = CursorLockMode.None;
    }
}
