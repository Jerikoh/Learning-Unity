using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContactTrigger : MonoBehaviour
{
    public static event Action EventPlayerContact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventPlayerContact?.Invoke();
        }
    }
}
