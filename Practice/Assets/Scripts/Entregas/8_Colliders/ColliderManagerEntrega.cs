using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagerEntrega : MonoBehaviour
{
    [SerializeField] Transform[] teleportPosition;
    [SerializeField] float timeToTeleport = 1f;
    float timeInPortal;
    
    private void OnCollisionEnter(Collision other) //estaba usando OnCollider, por eso no funcionaba, ni siquiera se que debe hacer
    {
        timeInPortal = 0f;
    }

    private void OnCollisionStay(Collision other)
    {
        timeInPortal += Time.deltaTime;
        if (timeInPortal >= timeToTeleport)
        {
            int positionIndex = Random.Range(0, 3);
            other.transform.position = teleportPosition[positionIndex].position;
        }
    }
}
