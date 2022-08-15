using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEntrega : MonoBehaviour
{
    string msg;

        private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colisión con " + other.gameObject.name);

        if (other.gameObject.GetComponent<ColliderManagerEntrega>() != null) 
        {
            Debug.Log("Colisión con " + other.gameObject.name +", contiene ColliderManagerEntrega");
        }
        else Debug.Log("Colisión con " + other.gameObject.name);
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
