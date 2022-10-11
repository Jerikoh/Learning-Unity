using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Mi proyecto/Scriptable Objects/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Range(0f, 200f)] public float damage = 0;
    [Range(0f, 10f)] public float attackDelay = 0; //before attack
    [Range(0f, 10f)] public float attackRate = 0; //after attack
    [Range(0f, 10f)] public float drawSpeed = 0; //at equip & unequip

    //se puede y conviene usar herencia para scriptable objects? []

    [Header("As melee:")]
    [Range(0f, 5f)] public float range = 0;
    [Range(0, 100)] public int durability = 0;
    [Range(0f, 50f)] public float knockBack = 0;

    [Header("As firegun:")]
    [Range(0, 200)] public int ammo = 0;
    [Range(0f, 100f)] public float precision = 0;
    [Range(0f, 10f)] public float reloadSpeed = 0;
    [Range(0f, 50f)] public float recoil = 0; //screen shake
    public bool automatic = true;
}
