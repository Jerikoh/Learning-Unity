using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    int durability;

    void Start()
    {
        durability = weapon.durability;
    }
}
