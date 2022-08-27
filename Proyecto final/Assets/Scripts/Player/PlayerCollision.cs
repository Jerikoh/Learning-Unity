using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float collDamageInterval = 1f;
    [SerializeField] int collDamage = 20;
    bool canCollDamage = true;

    private void OnTriggerEnter(Collider other) //atenti a la diferencia Collider Collision, peligroso
    {
        if (other.gameObject.CompareTag("Pick-ups/Keys"))
        {
            Destroy(other.gameObject);
            GameManager.Keys += 1;
        }
        if (other.gameObject.CompareTag("Pick-ups/Tubs"))
        {
            Destroy(other.gameObject);
            GameManager.Energy += 1f;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && canCollDamage)
        {
            canCollDamage = false;
            Invoke("ResetCollDamage", collDamageInterval);
            Invoke("DealCollDamage", 0);
        }
    }

    void ResetCollDamage()
    {
        canCollDamage = true;
    }

    void DealCollDamage()
    {
        GameManager.Health -= collDamage; //luego sera otro el metodo de efectuarlo (per creature hit, temporalizado)
    }
}

