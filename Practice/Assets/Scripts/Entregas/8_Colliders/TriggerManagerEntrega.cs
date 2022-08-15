using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagerEntrega : MonoBehaviour
{
    bool transformed = false;
    Collider collided;

    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerExit(Collider other) //ante el error del "doble trigger" ejecuto el comando con "timer" en un metodo aparte
    {
        collided = other;
        Invoke("Transformation", 0.05f);

    }

    void Transformation()
    {
        if (transformed == false)
        {
            collided.transform.localScale -= transform.localScale * 0.5f;
        }
        else if (transformed)
        {
            collided.transform.localScale += transform.localScale * 0.5f;
        }
        transformed = !transformed; //la transformacion sucede aqui, sucedia un inconveniente al ponerlo tras el invoke
    }
}
