using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class RandomMedkit : MonoBehaviour
{
    BoxCollider medCollider;
    public AudioClip myClip;
    public AudioClip myClip_ache;
    private AudioSource mySource;
    public float volume = 0.5f;
    public Text txtToDisplay; // ����� ��� ����������� ���������
    private bool playerInTriggerZone = false;
    private Collider playerCollider;
    private FirstPersonMovement playerMovement;
    private bool isPickedUp = false; // ���� ��� ��������, ���� �� ������� ��� ���������

    void Start()
    {
        mySource = GetComponent<AudioSource>();
        medCollider = GetComponent<BoxCollider>();
        medCollider.isTrigger = true;
        playerMovement = FindObjectOfType<FirstPersonMovement>(); // ����� ��������� FirstPersonMovement
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerInTriggerZone = true;
            playerCollider = otherCollider;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerCollider != null)
                {
                    PickUpMedKit(playerCollider.gameObject.GetComponent<LevelHealth>());
                }
            }
        }
    }

    void PickUpMedKit(LevelHealth playerHealth)
    {
        int HPAmount = Random.Range(10, 50);

        // ������� �� ��������
        int ChangeType = Random.Range(0, 5); // �������� �������� ��� ��������� ���������
        float randomModifier = Random.Range(-3.0f, 3.0f);// ������� �� ��������� ������

        switch (ChangeType)
        {
            case 0: // ���������� �������� �� �������� �� 10 �� 50 ������
                if ((playerHealth.levelHealth + HPAmount) < 100)
                    playerHealth.levelHealth += HPAmount;
                else
                    playerHealth.levelHealth = 100;
                break;

            case 1: // ���������� �������� �� �������� �� 10 �� 50 ������
                if ((playerHealth.levelHealth - HPAmount) > 0)
                    playerHealth.levelHealth -= HPAmount;
                else
                    playerHealth.levelHealth = 0;
                mySource.PlayOneShot(myClip_ache, volume);
                break;

            case 2: // ����������/���������� �������� �������� ����
                playerMovement.speed += randomModifier;
                if (playerMovement.speed <= 0)
                    playerMovement.speed = 1;
                break;

            case 3: // ����������/���������� �������� �������� ����
                playerMovement.runSpeed += randomModifier;
                if (playerMovement.runSpeed < 0)
                    playerMovement.speed = 0;
                break;

            case 4: // ����������� �������� ���� (���������)
                playerMovement.canRun = true;
                break;

            case 5: // ����������� �������� ���� (����������)
                playerMovement.canRun = false;
                break;
        }

        txtToDisplay.text = "��������� �������";

        mySource.PlayOneShot(myClip, volume);

        isPickedUp = true; // ������������� ����, ��� ������� ���������

        SetObjectVisibility(false);

        StartCoroutine(DeactivateWithDelay());
    }

    void SetObjectVisibility(bool isVisible)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }

    IEnumerator DeactivateWithDelay()
    {
        yield return new WaitForSeconds(0.7f);

        txtToDisplay.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
