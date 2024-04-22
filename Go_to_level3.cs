using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class Go_to_level3 : MonoBehaviour
{
    BoxCollider mCollider;
    public Text txtToDisplay; // ����� ��� ����������� ���������
    private bool playerInTriggerZone = false;
    private Collider playerCollider; // ��������� ���� ��� �������� ���������� ������

    void Start()
    {
        mCollider = GetComponent<BoxCollider>();
        mCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            playerCollider = otherCollider; // ��������� ��������� ������
            txtToDisplay.gameObject.SetActive(true);
            txtToDisplay.text = "������� �������";
        }
    }

    void Update()
    {
        if (playerInTriggerZone)
        {
            // ���������� ����������� ��������� ������
            if (playerCollider != null)
            {
                PickUpM();
            }
        }
    }

    void PickUpM()
    {
        // �������� ������
        SetObjectVisibility(false);

        // ��������� �������� ����� ������������� �����������
        StartCoroutine(DeactivateWithDelay());
    }

    void SetObjectVisibility(bool isVisible)
    {
        // �������� �������� �������
        Renderer renderer = GetComponent<Renderer>();

        // ���� � ������� ���� ��������, �������� ��� ���������
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }

    IEnumerator DeactivateWithDelay()
    {
        // ��������� ��������
        yield return new WaitForSeconds(1.0f);

        txtToDisplay.gameObject.SetActive(false);
        // ��������� ������
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene3");
    }
}