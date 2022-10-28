using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    //!! chanchada, no pude hacer funcionar todo junto (MeleeCollider + ColliderAux) ni con eventos
    bool canPushDamage = false;
    bool canSlashDamage = false;
    [SerializeField][Range(0, 2)] float pushDelay = 0.5f;
    [SerializeField][Range(0, 2)] float slashDelay = 0.5f;
    [SerializeField][Range(0, 2)] float pushDamageTime = 0.5f;
    [SerializeField][Range(0, 2)] float slashDamageTime = 0.5f;
    //estos valores deberias gestionarse desde el weaponmanager, pero mientras... []
    [SerializeField][Range(0, 20)] int pushDamage = 5;
    [SerializeField][Range(1, 50)] int slashDamage = 40;

    [SerializeField] GameObject colliderAuxPush;
    [SerializeField] GameObject colliderAuxSlash;

   

    // no entiendo por que ninguno de estos items que van a la mano no escuchan eventos! [] debe ser por como las trabaja el weaponmanager, pero ni idea
    /*void Start()
    {
        InputManager.EventPlayerPush += DoPush;
        InputManager.EventPlayerSlash += DoSlash;
    }

    void OnDisable()
    {
        InputManager.EventPlayerPush -= DoPush;
        InputManager.EventPlayerSlash -= DoSlash;
    }*/

     public void DoPush()
    {
        if (!canPushDamage)
        {
            Invoke("PushActivator", pushDelay);
        }
    }

    public void DoSlash()
    {
        if (!canSlashDamage)
        {
            Invoke("SlashActivator", slashDelay);
        }
    }

    void PushActivator()
    {
        canPushDamage = true;
        colliderAuxPush.SetActive(true);
        Invoke("StopPush", pushDamageTime);
        Debug.Log("canPushDamage");
    }

    void SlashActivator()
    {
        canSlashDamage = true;
        colliderAuxSlash.SetActive(true);
        Invoke("StopSlash", slashDamageTime);
        Debug.Log("canSlashDamage");
    }

    void StopPush()
    {
        canPushDamage = false;
        colliderAuxPush.SetActive(false);
        Debug.Log("canPush OFF");
    }

    void StopSlash()
    {
        canSlashDamage = false;
        colliderAuxSlash.SetActive(false);
        Debug.Log("canSlash OFF");
    }

    public void PushDamageDone()
    {
        colliderAuxPush.SetActive(false);
    }

    public void SlashDamageDone()
    {
        colliderAuxSlash.SetActive(false);
    }
}
