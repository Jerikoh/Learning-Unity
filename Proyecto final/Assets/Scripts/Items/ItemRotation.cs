using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField][Range(0, 100)] float XdegreesPerSec = 0;
    [SerializeField][Range(0, 100)] float YdegreesPerSec = 0;
    [SerializeField][Range(0, 100)] float ZdegreesPerSec = 0;
    //agregar animacion "ping pong" arriba abajo

    void Update()
    {
        //rotacion
        transform.Rotate(XdegreesPerSec, YdegreesPerSec, ZdegreesPerSec * Time.deltaTime);
    }
}
