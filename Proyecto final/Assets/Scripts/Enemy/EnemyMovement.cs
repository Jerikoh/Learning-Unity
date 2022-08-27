
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField][Range(1f, 10f)] float speed = 2f;
    [SerializeField][Range(1f, 10f)] float rotSmoothness = 7f;
    [SerializeField] enum EnemyType { Stalker, Crawler, Climber, Rioter }; //para tener un menu de seleccion con nombres y no numeros u otras ref inconcretas
    [SerializeField] EnemyType enemyType; //aca instancio el enum anteriormente definido
    [SerializeField] Transform playerTransform; //luego aprenderemos a buscar el player en runtime para evitar usar esta ref
    [SerializeField] Animator enemyAnimator; //para importar el componente de animation stuff
    Rigidbody enemyBody;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        switch (enemyType)
        {
            case EnemyType.Crawler:
                ChasePlayer();
                break;
            case EnemyType.Stalker:
                Debug.Log("Definido enemigo tipo Stalker, aún no implementado");
                break;
            case EnemyType.Climber:
                Debug.Log("Definido enemigo tipo Climber, aún no implementado");
                break;
            case EnemyType.Rioter:
                Debug.Log("Definido enemigo tipo Rioter, aún no implementado");
                break;
            default:
                Debug.Log("El tipo de enemigo no ha sido definido en el inspector.");
                break;
        }
    }

    void ChasePlayer()
    {
        LookAtPlayer();
        if (GetDirection().magnitude > 0f) //serializable, compruebo si la distancia es mayor a tal valor para evitar que se le superponga al player, aunque con rigidbody solo lo chocaria pero constantemente
        {
            enemyBody.AddForce(GetDirection().normalized * speed); //speed en realidad sería movementForce? [] todo este movimiento será pulido claro, en tanto tipos y animaciones
        }
    }

    void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(GetDirection(), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotSmoothness * Time.deltaTime); //implementado lerp
    }

    Vector3 GetDirection()
    {
        return playerTransform.position - transform.position; //pos player - pos este enemigo, resultando una direccion 
    }
}
