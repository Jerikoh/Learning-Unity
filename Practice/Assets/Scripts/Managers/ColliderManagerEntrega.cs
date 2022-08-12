using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManagerEntrega : MonoBehaviour
{
    [SerializeField] Transform[] teleportPosition;
    [SerializeField] float timeToTeleport = 1f;
    float timeInPortal;

    private void OnColliderEnter(Collider other)
    {
        timeInPortal = 0f;
        Debug.Log("COLLISION DETECTED");
    }

    private void OnColliderStay(Collider other)
    {
        Debug.Log("STAYING COLLISION");
        timeInPortal += Time.deltaTime;
        if (timeInPortal >= timeToTeleport)
        {
            int positionIndex = Random.Range(0, 3);
            other.transform.position = teleportPosition[positionIndex].position;
            Debug.Log("TELEPORT");
        }
    }
}
