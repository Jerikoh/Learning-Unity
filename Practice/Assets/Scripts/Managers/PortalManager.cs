using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] Transform refNextPortal;
    [SerializeField] float timeToTeleport = 2f;
    float timeInPortal;

    private void OnTriggerEnter(Collider other)
    {
        timeInPortal = 0f; //reinicio timer cada vez que entre al portal, para que no se acumule
    }

    private void OnTriggerStay(Collider other)
    {
        timeInPortal += Time.deltaTime; //me permite acumular el tiempo frame a frame que estuve en ese lugar
        if (timeInPortal >= timeToTeleport)
        {
            other.transform.position = refNextPortal.position; //hago que lo que colideo con el portal vaya a la posicion del otro portal
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
