using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponSO weapon;
    protected bool canAttack = true;

    void Attack()
    {
        if (canAttack)
        {
            Invoke("DelayedAttack", weapon.attackDelay);
        }
        else return;
    }

    void DelayedAttack()
    {
        canAttack = false;
        Debug.Log("Doing Attack");
        Invoke("CanAttackReset", weapon.attackRate);
    }

    protected void CanAttackReset()
    {
        canAttack = true;
    }

    protected void Equip()
    {
        Debug.Log("Equiped");
    }

    protected void Unequip()
    {
        Debug.Log("Unequiped");
    }
}
