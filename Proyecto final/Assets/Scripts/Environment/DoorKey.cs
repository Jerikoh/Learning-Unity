using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorKey : MonoBehaviour
{
    [SerializeField] AudioSource keyDoorOpen;
    public static event Action EventKeyDoorOpen;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.Keys > 0)
        {
            EventKeyDoorOpen?.Invoke();
            GameManager.Keys--;
            keyDoorOpen.Play();
            Invoke("Desactivate", 0.3f);
        }
    }

    void Desactivate()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
