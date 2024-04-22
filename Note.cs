using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Note : MonoBehaviour
{
    public string noteTextstr;
    public GameObject notice;
    public GameObject noteUI;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            text.text = noteTextstr;
            if (Input.GetKey(KeyCode.E))
            {
                noteUI.SetActive(true);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                noteUI.SetActive(false);
            }
            notice.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        notice.SetActive(false);
        noteUI.SetActive(false);
    }
}