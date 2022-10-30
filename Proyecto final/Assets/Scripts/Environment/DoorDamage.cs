using System;
using System.Collections;
using System.Collections.Generic;
using IndieMarc.EnemyVision;
using UnityEngine;

public class DoorDamage : MonoBehaviour
{
    //acá lo correcto sería meter todas las variables que competen al daño efectuado, velocidad, deteccion, tipo de movimiento, etc [] ++
    [Header("Door stats:")]
    [SerializeField] bool isOfKeyRoom = false;
    [SerializeField][Range(1, 100)] int health = 100;

    [Header("Door AudioSources:")]
    [SerializeField] AudioSource doorDamage;
    [SerializeField] AudioSource doorDeath;
    [SerializeField] AudioSource yeahPlayer;

    public static event Action DoorDestroyed;

    public void Damage(int weaponDamage)
    {
        if (health - weaponDamage > 0)
        {
            health -= weaponDamage;
            doorDamage.pitch = UnityEngine.Random.Range(0.8f, 1f);
            doorDamage.Play();

            Debug.Log("DOOR HITTED, hp: " + health);
        }
        else if (health - weaponDamage <= 0)
        {
            doorDamage.pitch = UnityEngine.Random.Range(0.8f, 1f);
            doorDamage.Play();
            doorDeath.pitch = UnityEngine.Random.Range(0.8f, 1f);
            doorDeath.Play();
            DoorDestroyed?.Invoke();
            Invoke("Desactivate", 0.5f);
        }
    }

    void Desactivate()
    {
        gameObject.SetActive(false);
        if (isOfKeyRoom)
        {
            yeahPlayer.Play();
        }
    }
}
