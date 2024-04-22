using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Light FL;
    public GameObject player;
    public AudioClip toggleSound;
    private AudioSource audioSource;
    public RawImage flashlightImage;
    public Text batteryText;

    public float batteryCharge = 100f;
    private float maxBatteryCharge = 100f;
    public float dischargeRate;//1f;

    private bool isFlashlightOn = false; // ���������� ��� ������������ ��������� ��������
    private bool isPaused = false; // ���������� ��� ������������ ��������� �����

    private void Start()
    {
        dischargeRate = 1f;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = toggleSound;
        FL.enabled = false;
        flashlightImage.enabled = false;
        UpdateBatteryText();
        StartCoroutine(ManageBatteryDischarge());
    }

    private void Update()
    {
        if (batteryCharge == 0)
        {
            FL.enabled = false;
            flashlightImage.enabled = false;
            isFlashlightOn = false;
        }

        // ��������� ������� ������� F � ��� ����� ������� ������ ���� � ���� �� �� �����
        if (Input.GetKeyDown(KeyCode.F) && batteryCharge > 0 && !isPaused)
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        if (batteryCharge > 0 && !isFlashlightOn) // ���������, ��� ����� ������� ������ ���� � ������� �� �������
        {
            FL.enabled = true;
            flashlightImage.enabled = true;
            isFlashlightOn = true;

            audioSource.PlayOneShot(toggleSound, 0.5f);
        }

        else if (isFlashlightOn) // ���� ������� ��� �������, ��������� ���
        {
            FL.enabled = false;
            flashlightImage.enabled = false;
            isFlashlightOn = false;

            audioSource.PlayOneShot(toggleSound, 0.5f);
        }
    }

    // ����� ��� ��������� ��������� ����� �����
    public void SetPaused(bool paused)
    {
        isPaused = paused;
    }

    public void RechargeBattery(float amount)
    {
        batteryCharge += amount;
        batteryCharge = Mathf.Clamp(batteryCharge, 0f, maxBatteryCharge);
        UpdateBatteryText();
    }

    private void UpdateBatteryText()
    {
        batteryText.text = "����� ��������: " + Mathf.RoundToInt(batteryCharge) + "%";
    }

    private IEnumerator ManageBatteryDischarge()
    {
        if (batteryCharge == 0)
        {
            FL.enabled = false;
            flashlightImage.enabled = false;
            isFlashlightOn = false;
        }

        if (batteryCharge > 0)
        {
            while (true)
            {
                float seconds = Random.Range(1f, 5f);
                yield return new WaitForSeconds(seconds);
                if (!isPaused && isFlashlightOn) // ���������, �� ��������� �� ���� �� ����� � ������� �� �������
                {
                    batteryCharge -= dischargeRate;
                    batteryCharge = Mathf.Clamp(batteryCharge, 0f, maxBatteryCharge);
                    UpdateBatteryText();
                }
            }
        }

        if (batteryCharge == 0)
        {
            FL.enabled = false;
            flashlightImage.enabled = false;
            isFlashlightOn = false;
        }
    }
}
