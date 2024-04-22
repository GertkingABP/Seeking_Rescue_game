/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    [SerializeField] Animator DoorAnimator;
    [SerializeField] bool Closed;

    public void TryOpen()
    {
        if (!Closed)
        {
            if (DoorAnimator.GetBool("isOpen") == false)
            {
                DoorAnimator.SetBool("isOpen", true);
            }

            else
            {
                DoorAnimator.SetBool("isOpen", false);
            }

        }
    }

    public void Unlock()
    {
        Closed = false;
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    [SerializeField] Animator DoorAnimator;
    [SerializeField] bool Closed;

    public void TryOpen()
    {
        if (Closed)
        {
            if (DoorAnimator.GetBool("isOpen") == false)
            {
                DoorAnimator.SetBool("isOpen", true);
                Closed = false; // Update the door state after opening
            }
        }
        else
        {
            DoorAnimator.SetBool("isOpen", false);
            Closed = true; // Update the door state after closing
        }
    }

    public bool IsClosed()
    {
        return Closed;
    }

    public void Unlock()
    {
        Closed = false;
    }
}