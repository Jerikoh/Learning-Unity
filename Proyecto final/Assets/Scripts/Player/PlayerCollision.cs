using System;
using System.Collections;
using System.Collections.Generic;
using IndieMarc.EnemyVision;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //creo que estos efectos deberian llevarse desde el enemigo/criatura, [] como que debe seguirse una logica efectuador->efectuado y la ejecucion de los efectos estar en el efectuador
    [SerializeField] float creatureDamageInterval = 1f;
    [SerializeField] int creatureDamage = 20;
    [SerializeField][Range(0f, 3f)] float pickupEquipDelay = 0.7f;
    bool canCollDamage = true;
    Collider otherLast;

    //para acceder al script del GO WeaponManager, no se si es la mejor forma [] hay otras cosas que usar de c# para esto?
    [SerializeField] GameObject weaponManagerGO;
    [SerializeField] Animator playerAnimator; //si me organizo mejor , esto podria ser un manager []
    [SerializeField] SoundManager soundManagerGO; //tampoco deberia estar aqui []

    //para evitar que se reiteren algunas "death lines"
    bool playerAlive = true;

    //EVENTOS
    public static event Action EventPlayerDamage; //esto deberia ponerlo en un EventManager []
    public static event Action EventPlayerDeath;

    private void OnTriggerEnter(Collider other) //atenti a la diferencia Collider Collision, peligroso
    {
        otherLast = other;
        //other.GetComponent<SphereCollider>().enabled = false;
        if (other.TryGetComponent(out SphereCollider sCollider)) //no se por que abajo funciona el "getcomponent?" y aca no //considerar que estaria reservando el spherecollider a los items pickeables
        {
            sCollider.enabled = false;
            playerAnimator.SetTrigger("Pick-up"); //!! al menos darle un delay al destroy/equip como para sumarle al efecto de recoger
            Invoke("DoPickup", pickupEquipDelay);
        }
    }

    void DoPickup()
    {
        //consumibles
        if (otherLast.gameObject.CompareTag("Pick-ups/Keys"))
        {
            Destroy(otherLast.gameObject);
            GameManager.Keys += 1;
        }
        if (otherLast.gameObject.CompareTag("Pick-ups/Tubs"))
        {
            Destroy(otherLast.gameObject);
            GameManager.Energy += 1f; //esto mas adelante deberia cambiar para hacerlo consumible de forma manual y no no-pickup []
        }
        //armas
        //desde acá podría determinar la animacion tambien? para variar el idle holding melee/pistol/rifle, again no se si es la forma mas ordenada de hacerlo, pero me parece que pensandolo como eventos (pick up) esta bueno que se le asocie a este el acto de definir los efectos de agarrar tal cosa (cambio de animacion, modelo, etc); se podría pensar en tanto "QUÉ-CONQUÉ-COMO-CONQUIÉN-etc"
        if (otherLast.gameObject.CompareTag("Pick-ups/Weapons/Spanner"))
        {
            Destroy(otherLast.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(0);
        }
        if (otherLast.gameObject.CompareTag("Pick-ups/Weapons/Knife"))
        {
            Destroy(otherLast.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(1);
        }
        if (otherLast.gameObject.CompareTag("Pick-ups/Weapons/Hatchet"))
        {
            Destroy(otherLast.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(2);
        }
        if (otherLast.gameObject.CompareTag("Pick-ups/Weapons/Revolver"))
        {
            Destroy(otherLast.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(3);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && canCollDamage)
        {
            canCollDamage = false;
            Invoke("ResetCollDamage", creatureDamageInterval);
            Invoke("DealCollDamage", 0); //esto no deberia ir antes del reset? probar []

            other.gameObject.GetComponent<Animator>()?.SetTrigger("Bite"); //consulta si tiene componente, una alternativa al trygetcomponent, no se con que desventajas mas que no poder definir else y mas comandos
        }
    }

    void ResetCollDamage()
    {
        canCollDamage = true;
    }

    void DealCollDamage()
    {
        if (GameManager.Health - creatureDamage <= 0 && playerAlive)
        {
            playerAlive = false;
            GameManager.Health = 0;
            Invoke("DoDeath", 3f); //en realidad deberia hacer que los efectos en respuesta al evento tengan delay, y no ponerle delay al invoke del evento [] 
            playerAnimator.SetTrigger("Death"); //!! haria un "death manager", que efectue animaciones, fades, sonidos, UI, controller off, collider off, visibilidad off, scene restart []
            soundManagerGO.PlayDeath();
            gameObject.GetComponent<VisionTarget>().visible = false;
            gameObject.GetComponent<InputManager>().enabled = false;
        }
        else
        {
            GameManager.Health -= creatureDamage; //luego sera otro el metodo de efectuarlo (per creature hit, temporalizado)
            playerAnimator.SetTrigger("Damage");
        }
        EventPlayerDamage?.Invoke();
    }

    void DoDeath()
    {
        //esto es un intento de fix ante error al recargar nivel tras muerte
        WeaponManager.Weapon1 = false;
        WeaponManager.Weapon2 = false;
        WeaponManager.Weapon3 = false;
        WeaponManager.Weapon4 = false;
        WeaponManager.EquippedItem = 5;

        EventPlayerDeath?.Invoke(); //invoco el evento si es que alguien escucha
    }
}

