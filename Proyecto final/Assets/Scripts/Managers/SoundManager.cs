using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player AudioSources:")]
    [SerializeField] AudioSource playerDeath;
    [SerializeField] AudioSource playerBreathingIdle;
    [SerializeField] AudioSource playerEquip;
    [SerializeField] AudioSource playerPickUp;
    [SerializeField] AudioSource playerDamage1;
    [SerializeField] AudioSource playerDamage2;
    [SerializeField] AudioSource playerDamage3;
    [SerializeField] AudioSource playerDamage4;
    [SerializeField] AudioSource firstContact;
    [SerializeField] AudioSource firstKill;
    [SerializeField] AudioSource secondKill;
    [SerializeField] AudioSource panic;
    [Header("Revolver AudioSources:")]
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverReload;
    [SerializeField] AudioSource revolverPullTrigger;
        [Header("Items AudioSources:")]
    [SerializeField] AudioSource itemHealth;
    [SerializeField] AudioSource itemAmmo;
    [SerializeField] AudioSource itemEnergy;
    [SerializeField] AudioSource itemKeys;
    /*
    [Header("Hatchet AudioSources:")]
    [Header("Creature AudioSources:")]
    [SerializeField] AudioSource axeSlash;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverShoot;
    */

    bool firstKillDone = false;
    bool secondKillDone = false;

    void Start()
    {
        WeaponManager.EventPlayerReload += PlayReload;
        WeaponManager.EventPlayerShoot += PlayShoot;
        PlayerCollision.EventPlayerDamage += PlayDamage;
        EnemyManager.enemyDeath += PlaySecondKill; //el orden de los llamados importa! sino se ejecutan todos en una sola invocacion; sino deberian responder a eventos distintos, o a un contador de killed enemies (if == 2 then)
        EnemyManager.enemyDeath += PlayFirstKill;

        playerBreathingIdle.PlayDelayed(5f);
    }

    void OnDisable()
    {
        WeaponManager.EventPlayerReload -= PlayReload;
        WeaponManager.EventPlayerShoot -= PlayShoot;
        PlayerCollision.EventPlayerDamage -= PlayDamage;
        EnemyManager.enemyDeath -= PlayFirstKill;
        EnemyManager.enemyDeath -= PlaySecondKill;
    }

    void PlayReload() //quisiera trabajar con un solo método para reproducir todos los sonidos que se indiquen con un index o nombre, no recuerdo como se llamaba, enum? []
    {
        revolverReload.PlayDelayed(0.3f);
    }

    void PlayShoot()
    {
        revolverShoot.pitch = UnityEngine.Random.Range(0.7f, 1f);
        revolverShoot.Play();
    }

    void PlayDamage()
    {
        if (GameManager.Health <= 80 && GameManager.Health > 60) playerDamage1.Play();
        if (GameManager.Health <= 60 && GameManager.Health > 40) playerDamage2.Play();
        if (GameManager.Health <= 40 && GameManager.Health > 20) playerDamage3.Play();
        if (GameManager.Health <= 20 && GameManager.Health > 0) playerDamage4.Play();
    }

    void PlayFirstKill()
    {
        if (!firstKillDone)
        {
            firstKillDone = true;
            firstKill.PlayDelayed(2f); //podria editarse desde el inspector []
        }
    }

    void PlaySecondKill()
    {
        if (!secondKillDone && firstKillDone)
        {
            secondKillDone = true;
            secondKill.PlayDelayed(0f); //podria editarse desde el inspector []
        }
    }

    public void PlayDeath()
    {
        playerDeath.Play();
    }

    public void PlayTrigger()
    {
        revolverPullTrigger.Play();
    }

    public void PlayContact()
    {
        firstContact.Play();
    }

    public void PlayPanic()
    {
        panic.Play();
    }

    public void PlayPickUp()
    {
        playerPickUp.Play();
    }

    public void PlayEquip()
    {
        playerEquip.Play();
    }

    public void PlayItemHealth()
    {
        itemHealth.Play();
    }

    public void PlayItemAmmo()
    {
        itemAmmo.Play();
    }

    public void PlayItemEnergy()
    {
        itemEnergy.Play();
    }

    public void PlayItemKeys()
    {
        itemKeys.Play();
    }
}
