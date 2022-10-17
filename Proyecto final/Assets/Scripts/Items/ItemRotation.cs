using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float XdegreesPerSec = 0;
    [SerializeField][Range(0f, 10f)] float YdegreesPerSec = 0;
    [SerializeField][Range(0f, 10f)] float ZdegreesPerSec = 0;

    //agregar animacion "ping pong" arriba abajo, DONE
    [SerializeField][Range(0f, 10f)] float amplitude, frequency;

    void FixedUpdate()
    {
        //rotacion
        transform.Rotate(XdegreesPerSec, YdegreesPerSec, ZdegreesPerSec * Time.fixedDeltaTime);

        //up-down sine
        transform.localPosition = new Vector3(transform.localPosition.x, MathF.Sin(Time.fixedTime * frequency) * amplitude + transform.localPosition.y, transform.localPosition.z);
    }
}
