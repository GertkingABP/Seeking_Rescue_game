using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondLevelStart : MonoBehaviour
{
    public void SecondPressed()
    {
        SceneManager.LoadScene("SampleScene2");
    }
}