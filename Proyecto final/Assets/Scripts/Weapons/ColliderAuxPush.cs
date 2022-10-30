using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAuxPush : MonoBehaviour
{

    [SerializeField] MeleeCollider meleeCollider; //esto debo hacerlo ahora porque no podia hacerlo funcionar todo junto en MeleeCollider, no pude enterder porque
    [SerializeField] AudioSource audioPushed;

    void OnCollisionStay(Collision other)
    {
        Debug.Log("COLISION DETECTADA al menos");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.gameObject.GetComponent<EnemyManager>().Damage(5); //aca al menos deberia agarrar el valor desde una propiedad de MeleeCollider
            meleeCollider.PushDamageDone();
            audioPushed.pitch = UnityEngine.Random.Range(0.7f, 0.9f);
            audioPushed.Play();
            Debug.Log("PUSHEADO");
        }
    }
}
