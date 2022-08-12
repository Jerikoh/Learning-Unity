using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //cuando entra en colision (una sola vez), other es el otro objeto con el que entró en colision (de a uno?)
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Entrando en colisión con " + other.gameObject.name); //obtengo el GO del otro, pudiendo a traves del mismo acceder a sus componentes? ej .transform.position

        if (other.gameObject.CompareTag("Pick-ups")) //consulto el tag del GO colideado
        {
            Destroy(other.gameObject);
        }

    }
    //util para eliminar objetos al tocarlos, con un condicional pero sin usar el nombre, ya veremos como

    //cuando salgo del estado de colision
    private void OnCollisionExit(Collision other)
    {

    }

    //cuando me mantengo en colision
    private void OnCollisionStay(Collision other)
    {

    }

}
