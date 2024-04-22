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
        // Проверка нажатия клавиши Q для закрытия двери
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = false;
            m_Animator.SetBool("isOpen", false);
        }
    }

    public string GetDescription()
    {
        if (isOpen)
            return "Нажмите Q чтобы <color=red>закрыть</color> дверь";
        return "Нажмите E чтобы <color=green>открыть</color> дверь";
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