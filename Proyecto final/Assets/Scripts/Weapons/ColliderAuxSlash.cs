using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAuxSlash : MonoBehaviour
{

    [SerializeField] MeleeCollider meleeCollider; //esto debo hacerlo ahora porque no podia hacerlo funcionar todo junto en MeleeCollider, no pude enterder porque
    [SerializeField] AudioSource audioSlashed;

    void OnCollisionStay(Collision other)
    {
        Debug.Log("COLISION DETECTADA al menos");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.gameObject.GetComponent<EnemyManager>().Damage(40); //aca al menos deberia agarrar el valor desde una propiedad de MeleeCollider
            meleeCollider.SlashDamageDone();
            audioSlashed.Play();
            Debug.Log("SLASHEADO");
        }
        if (other.gameObject.CompareTag("Doors"))
        {
            other.transform.gameObject.GetComponent<DoorDamage>().Damage(40); //aca al menos deberia agarrar el valor desde una propiedad de MeleeCollider
            meleeCollider.SlashDamageDone();

        }
    }
}
