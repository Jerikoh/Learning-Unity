using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContactTrigger : MonoBehaviour
{
    public static event Action EventPlayerContact;
    bool wasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !wasTriggered)
        {
            wasTriggered = true;
            EventPlayerContact?.Invoke();
        }
    }
}
