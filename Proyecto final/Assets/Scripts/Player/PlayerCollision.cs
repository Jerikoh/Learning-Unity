using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //creo que estos efectos deberian llevarse desde el enemigo/criatura, [] como que debe seguirse una logica efectuador->efectuado y la ejecucion de los efectos estar en el efectuador
    [SerializeField] float creatureDamageInterval = 1f;
    [SerializeField] int creatureDamage = 20;
    bool canCollDamage = true;

    //para acceder al script del GO WeaponManager, no se si es la mejor forma [] hay otras cosas que usar de c# para esto?
    [SerializeField] GameObject weaponManagerGO;

    private void OnTriggerEnter(Collider other) //atenti a la diferencia Collider Collision, peligroso
    {
        //consumibles
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
        //armas
        //desde acá podría determinar la animacion tambien? para variar el idle holding melee/pistol/rifle, again no se si es la forma mas ordenada de hacerlo, pero me parece que pensandolo como eventos (pick up) esta bueno que se le asocie a este el acto de definir los efectos de agarrar tal cosa (cambio de animacion, modelo, etc); se podría pensar en tanto "QUÉ-CONQUÉ-COMO-CONQUIÉN-etc"
        if (other.gameObject.CompareTag("Pick-ups/Weapons/Spanner"))
        {
            Destroy(other.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(0);
            GameManager.Weapon1 = true; //para señalar posesion de tal arma
        }
        if (other.gameObject.CompareTag("Pick-ups/Weapons/Knife"))
        {
            Destroy(other.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(1);
            GameManager.Weapon2 = true;
        }
        if (other.gameObject.CompareTag("Pick-ups/Weapons/Hatchet"))
        {
            Destroy(other.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(2);
            GameManager.Weapon3 = true;
        }
        if (other.gameObject.CompareTag("Pick-ups/Weapons/Revolver"))
        {
            Destroy(other.gameObject);
            weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(3);
            GameManager.Weapon4 = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && canCollDamage)
        {
            canCollDamage = false;
            Invoke("ResetCollDamage", creatureDamageInterval);
            Invoke("DealCollDamage", 0); //esto no deberia ir antes del reset? probar []
        }
    }

    void ResetCollDamage()
    {
        canCollDamage = true;
    }

    void DealCollDamage()
    {
        GameManager.Health -= creatureDamage; //luego sera otro el metodo de efectuarlo (per creature hit, temporalizado)
    }
}

