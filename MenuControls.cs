using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void ExitToMenuPressed()
    {
        SceneManager.LoadScene("menu");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Выход из игры выполнен");
    }
}