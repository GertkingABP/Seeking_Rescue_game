using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class MedKit : MonoBehaviour
{
    BoxCollider medCollider;
    public AudioClip myClip;
    private AudioSource mySource;
    public float volume = 0.5f;
    public Text txtToDisplay; // ����� ��� ����������� ���������
    private bool playerInTriggerZone = false;
    private Collider playerCollider; // ��������� ���� ��� �������� ���������� ������
    private bool isPickedUp = false; // ���� ��� ��������, ���� �� ������� ��� ���������

    void Start()
    {
        mySource = GetComponent<AudioSource>();
        medCollider = GetComponent<BoxCollider>();
        medCollider.isTrigger = true;
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            playerCollider = otherCollider; // ��������� ��������� ������
            txtToDisplay.gameObject.SetActive(true);
            txtToDisplay.text = "������� E, ����� ��������� �������";
        }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = false;
            txtToDisplay.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInTriggerZone && !isPickedUp)
        {
            // ���������, ������ �� ������ E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ���������� ����������� ��������� ������
                if (playerCollider != null)
                {
                    LevelHealth playerHealth = playerCollider.gameObject.GetComponent<LevelHealth>();

                    // ��������� �������� �������� �������� ������
                    if (playerHealth != null && playerHealth.levelHealth < 100)
                    {
                        PickUpMedKit(playerHealth);
                    }
                }
            }
        }
    }

    void PickUpMedKit(LevelHealth playerHealth)
    {
        int HPAmount = Random.Range(10, 50);

        if ((playerHealth.levelHealth + HPAmount) < 100)
        {
            playerHealth.levelHealth += HPAmount;
        }

        if ((playerHealth.levelHealth + HPAmount) >= 100)
        {
            playerHealth.levelHealth = 100;
        }

        txtToDisplay.text = "��������� �������";

        mySource.PlayOneShot(myClip, volume);

        isPickedUp = true; // ������������� ����, ��� ������� ���������

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
        // ��������� �������� � 1.5 �������
        yield return new WaitForSeconds(0.7f);

        txtToDisplay.gameObject.SetActive(false);
        // ��������� ������
        gameObject.SetActive(false);
    }
}
