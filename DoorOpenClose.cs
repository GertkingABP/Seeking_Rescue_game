using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour, IInteractable
{
    public Animator m_Animator;
    public bool isOpen;

    void Start()
    {
        if (isOpen)
            m_Animator.SetBool("isOpen", true);
    }

    void Update()
    {
        // �������� ������� ������� Q ��� �������� �����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = false;
            m_Animator.SetBool("isOpen", false);
        }
    }

    public string GetDescription()
    {
        if (isOpen)
            return "������� Q ����� <color=red>�������</color> �����";
        return "������� E ����� <color=green>�������</color> �����";
    }

    public void Interact()
    {
        isOpen = !isOpen;
        if (isOpen)
            m_Animator.SetBool("isOpen", true);
        else
            m_Animator.SetBool("isOpen", false);
    }
}