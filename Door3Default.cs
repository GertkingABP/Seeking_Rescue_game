using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3Default : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private float distance;
    [SerializeField] private GameObject cam;
    [SerializeField] private string doorTag = "Door"; // ��� ��� ������

    private Animator doorAnimator;
    private bool isDoorOpen = false;

    public AudioClip doorOpenSound; // ���� �������� �����
    public AudioClip doorCloseSound; // ���� �������� �����
    private AudioSource audioSource;

    public float doorOpenVolume = 0.5f; // ��������� �������� �����
    public float doorCloseVolume = 0.5f; // ��������� �������� �����

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out hit, distance, layer))
        {
            if (hit.collider.CompareTag(doorTag))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ToggleDoor();
                }
            }
        }
    }

    void ToggleDoor()
    {
        if (doorAnimator != null)
        {
            isDoorOpen = !isDoorOpen; // ����������� ������� ��������� �����

            doorAnimator.SetBool("isOpen", isDoorOpen); // ������������� �������� �������� ��� ���� �����

            // ������������� ���� � ����������� �� ��������� �����
            if (isDoorOpen)
            {
                audioSource.PlayOneShot(doorOpenSound, doorOpenVolume);
            }
            else
            {
                audioSource.PlayOneShot(doorCloseSound, doorCloseVolume);
            }
        }
    }
}
