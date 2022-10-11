using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //cuando son referencias fijas, definimos desde el Inspector, al no ser dinamico o variar luego
    [SerializeField] GameObject[] weapons;
    [SerializeField] Transform playerHand;

    void Start()
    {
        EquipWeapon(3); //solo para demostracion []
    }

    void DisableWeapons()
    {
        foreach (GameObject index in weapons)
        {
            index.SetActive(false);
        }
    }

    public void EquipWeapon(int index) //esto se llama desde el PLAYERCOLLISION al triggear el item, el metodo es public para que eso sea posible
    {
        //desactivamos todas las armas para que no se superpongan; [] CONSIDERAR el tema de la conservacion de armas via GAMEMANAGER
        DisableWeapons();

        //activamos la primer arma
        weapons[index].SetActive(true);

        //agrego el arma a la palma del player
        weapons[index].transform.parent = playerHand; //reparenting!

        //para ajustar la posicion del objeto en relacion al nuevo padre, ahora local
        weapons[index].transform.localPosition = Vector3.zero;
        weapons[index].transform.localScale = new Vector3(1f, 1f, 1f); //correccion por redimension de player creo?
        weapons[index].transform.localRotation = Quaternion.Euler(0, 0, 0); //same
    }

    public void CheckAmmo() //* [] aca deberia pasar un parametro para consultar especificamente la municion de cada arma
    {
        string ammoInform = "RevolverAmmo: " + GameManager.PlayerAmmo["RevolverAmmo"] + " | ShotgunAmmo: " + GameManager.PlayerAmmo["ShotgunAmmo"] + " | RifleAmmo: " + GameManager.PlayerAmmo["RifleAmmo"];
        Debug.Log(ammoInform);
    }

}
