
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField][Range(1f, 5f)] float speed;
    [SerializeField][Range(1f, 50f)] float rotationSpeed = 40f; //estaria bueno que esto no se muestre al tipo Chaser pero bueno
    [SerializeField] enum EnemyType { Stalker, Rioter }; //para tener un menu de seleccion con nombres y no numeros u otras ref inconcretas
    [SerializeField] EnemyType enemyType; //aca instancio el enum que defini antes
    [SerializeField] Transform playerTransform; //luego aprenderemos a buscar el player en runtime para evitar usar esta ref
    [SerializeField][Range(1f, 10f)] float rotSmoothness = 7f;
    void Start()
    {

    }


    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Rioter:
                ChasePlayer();
                break;
            case EnemyType.Stalker:
                RotateAroundPlayer();
                break;
            default:
                Debug.Log("El tipo de enemigo no ha sido definido.");
                break;
        }
    }

    void ChasePlayer()
    {
        LookAtPlayer();
        if (GetDirection().magnitude > 2f) //compruebo si la distancia es mayor a 1 para evitar que se le superponga al player
        {
            transform.position += GetDirection().normalized * speed * Time.deltaTime; //si quiero que mire al player y rote bien debo usar .position y no .Translate
        }
    }

    void RotateAroundPlayer()
    {
        LookAtPlayer();
        transform.RotateAround(playerTransform.position, Vector3.up, rotationSpeed * Time.deltaTime); //puntoref,direccion,grados (per frame?); practicamente unity lo hace solo
        //debo implementarlo con lerp tambien?
    }

    void LookAtPlayer()
    {
        //transform.LookAt(playerTransform); //este metodo rota al transform que le pases, tiene varias formas de usarse
        //transform.rotation = Quaternion.LookRotation(GetDirection(), Vector3.up); //same pero con quaternion
        Quaternion targetRotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotSmoothness * Time.deltaTime); //implementado lerp
    }

    Vector3 GetDirection()
    {
        return playerTransform.position - transform.position; //pos player - pos este enemigo, resultando una direccion 
    }
}
