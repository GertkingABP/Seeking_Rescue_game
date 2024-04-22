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
            
            //для рестарта(не используется)
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnEnable()
    {
        // Показываем курсор при загрузке новой сцены
        Cursor.visible = true;
        // Разблокируем курсор при загрузке новой сцены
        Cursor.lockState = CursorLockMode.None;
    }
}
