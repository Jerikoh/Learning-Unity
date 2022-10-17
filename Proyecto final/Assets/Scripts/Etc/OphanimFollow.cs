using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OphanimFollow : MonoBehaviour
{

    [SerializeField] Transform playerTransform; //luego aprenderemos a buscar el player en runtime para evitar usar esta ref
    [SerializeField][Range(1f, 10f)] float rotationSmoothness = 7f;

    void Update()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime); //implementado lerp
    }

    Vector3 GetDirection()
    {
        return playerTransform.position - transform.position; //pos player - pos este enemigo, resultando una direccion 
    }
}
