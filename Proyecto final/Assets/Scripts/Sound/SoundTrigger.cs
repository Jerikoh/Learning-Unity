using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
