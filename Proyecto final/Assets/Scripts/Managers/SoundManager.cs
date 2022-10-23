using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player AudioSources:")]
    [SerializeField] AudioSource playerDeath;
    [Header("Revolver AudioSources:")]
    [SerializeField] AudioSource revolverShoot;
    [SerializeField] AudioSource revolverReload;
    [SerializeField] AudioSource revolverPullTrigger;
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

    void Start()
    {
        WeaponManager.EventPlayerReload += PlayReload;
        WeaponManager.EventPlayerShoot += PlayShoot;
    }

    void OnDisable()
    {
        WeaponManager.EventPlayerReload -= PlayReload;
        WeaponManager.EventPlayerShoot -= PlayShoot;
    }

    void PlayReload() //quisiera trabajar con un solo método para reproducir todos los sonidos que se indiquen con un index o nombre, no recuerdo como se llamaba, enum? []
    {
        revolverReload.PlayDelayed(0.3f);
    }

    void PlayShoot()
    {
        revolverShoot.Play();
    }

    public void PlayDeath()
    {
        playerDeath.Play();
    }

    public void PlayTrigger()
    {
        revolverPullTrigger.Play();
    }
}
