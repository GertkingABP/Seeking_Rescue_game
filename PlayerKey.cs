using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] KeyCode PickUp;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.tag == "Key")
            {
                if (Input.GetKeyDown(PickUp))
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor();
                }
            }

            if (hit.collider.tag == "Door")
            {
                if (Input.GetKeyDown(PickUp))
                {
                    hit.collider.gameObject.GetComponent<DoorEvent>().TryOpen();
                }
            }
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] KeyCode pickUpKey = KeyCode.E;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (Input.GetKeyDown(pickUpKey))
            {
                other.gameObject.GetComponent<KeyEvent>().UnlockDoor();
                Destroy(other.gameObject); // Assuming you want to destroy the key after picking it up
            }
        }
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [SerializeField] KeyCode PickUp;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.tag == "Key")
            {
                if (Input.GetKeyDown(PickUp))
                {
                    hit.collider.gameObject.GetComponent<KeyEvent>().UnlockDoor();
                }
            }

            if (hit.collider.tag == "Door")
            {
                if (Input.GetKeyDown(PickUp))
                {
                    DoorEvent doorEvent = hit.collider.gameObject.GetComponent<DoorEvent>();
                    if (doorEvent != null && doorEvent.IsClosed()) // Check if the door is closed before unlocking
                    {
                        doorEvent.TryOpen();
                    }
                }
            }
        }
    }
}*/