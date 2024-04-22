using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLevelStart : MonoBehaviour
{
    public void FirstPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
