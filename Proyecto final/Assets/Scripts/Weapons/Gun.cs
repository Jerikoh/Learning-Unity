using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    int loadedAmmo;
    int ammoLoads = 0;

    void Start()
    {
        loadedAmmo = weapon.ammo;
    }

    void Reload()
    {
        Debug.Log("Recargando en " + weapon.reloadSpeed);
    }
}
