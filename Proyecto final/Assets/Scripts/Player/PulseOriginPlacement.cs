using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseOriginPlacement : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField][Range(0f, 10f)] float chaseSpeed;

    void FixedUpdate()
    {
        // [] quizá logrando que el shader solo realice un solo pulso y no uno tras otro... ver si puedo usar tambien este otro que era basado en rebote, aunque creo que habia q colocarse lo a todo lo que pretendia que sea recorrido, pero al menos para ver como fue construido
        // [] debo hacer esto mas controlable, el tiempo en cero (ver como limitar la curva neg) y un aumento exponencial
        // ver https://answers.unity.com/questions/8781/oscillating-variable.html https://forum.unity.com/threads/simple-oscillation-of-a-variable.503906/ https://stackoverflow.com/questions/44851382/unity-oscillate-between-two-coordinates
        chaseSpeed = Math.Abs(Mathf.PingPong(Time.time, 2) * 2);
        ChasePlayer();
    }

    void ChasePlayer()
    {
        transform.position += GetDirection().normalized * chaseSpeed * Time.fixedDeltaTime;
    }

    Vector3 GetDirection()
    {
        return player.transform.position - transform.position;
    }
}
