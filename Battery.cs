using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Battery : MonoBehaviour
{
    private float chargeAmount; // ���������� ������, ������� ����������� � ��������
    public AudioClip pickupSound; // ���� ������� ���������
    public Text pickupText; // ����� ��� ����������� ��������� � ������� ���������
    public GameObject player; // ����� (����� ����� ������������ ��� ��� ������ ������)

    private FlashLight flashlight; // ������ �� ������ ��������
    private bool canPickup = false; // ���� ��� �������� ����������� ������� ���������
    private bool isPickedUp = false; // ���� ��� ��������, ���� �� ��������� ��� ���������

    private void Start()
    {
        flashlight = player.GetComponent<FlashLight>(); // �������� ��������� FlashLight �� ������
        HidePickupText(); // �������� � ������� ������
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && !isPickedUp && flashlight.batteryCharge < 100) // ���� ����� � ����, ������ ������ E, ��������� ��� �� ��������� � ����� ������ 100%
        {
            PickupBattery();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) // ���������, ��� ����������� � �������
        {
            canPickup = true; // ��������� ������ ���������
            ShowPickupText(); // ���������� ����� ���������
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player) // ���������, ��� ����� ������� ����
        {
            canPickup = false; // ��������� ������ ���������
            HidePickupText(); // �������� ����� ���������
        }
    }

    private void ShowPickupText()
    {
        pickupText.gameObject.SetActive(true); // �������� ������ ������
        pickupText.text = "������� E, ����� ���������"; // ������������� �����
    }

    private void HidePickupText()
    {
        pickupText.gameObject.SetActive(false); // ��������� ������ ������
    }

    private void PickupBattery()
    {
        StartCoroutine(PickupSequence()); // ��������� ��������
    }

    private IEnumerator PickupSequence()
    {
        isPickedUp = true; // ������������� ����, ��� ��������� ���������
        chargeAmount = Random.Range(10, 100);
        pickupText.text = "��������� ���������"; // ������������� �����
        AudioSource.PlayClipAtPoint(pickupSound, transform.position); // ������������� ���� ������� ���������
        pickupText.gameObject.SetActive(true); // �������� ������ ������
        yield return new WaitForSeconds(0.7f);
        flashlight.RechargeBattery(chargeAmount); // ��������� ����� ��������
        gameObject.SetActive(false); // ��������� ������ ���������
        pickupText.gameObject.SetActive(false); // ��������� ������ ������
    }
}
