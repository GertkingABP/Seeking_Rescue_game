using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
}