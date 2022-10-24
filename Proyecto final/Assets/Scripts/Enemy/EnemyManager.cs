using System;
using System.Collections;
using System.Collections.Generic;
using IndieMarc.EnemyVision;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Animator creatureAnimator;

    //acá lo correcto sería meter todas las variables que competen al daño efectuado, velocidad, deteccion, tipo de movimiento, etc [] ++
    [Header("Enemy stats:")]
    [SerializeField][Range(1, 1000)] int health = 100;
    [SerializeField][Range(0f, 5f)] float stunnedTime = 0.7f;

    [Header("Enemy AudioSources:")]
    //[SerializeField] AudioSource creatureAlert; //[]
    [SerializeField] AudioSource creatureDamage1;
    [SerializeField] AudioSource creatureDamage2;
    [SerializeField] AudioSource creatureDamage3;
    [SerializeField] AudioSource creatureAttack;
    [SerializeField] AudioSource creatureDeath;
    [Header("Other configs:")]
    [SerializeField][Range(0f, 10f)] float bodyRemovalTime = 3f;

    public static event Action enemyDeath;

    float regularRunSpeed;

    void Start()
    {
        regularRunSpeed = gameObject.GetComponent<Enemy>().run_speed;
        PlayerCollision.EventPlayerDamage += PlayAttack;
    }

    void OnDisable()
    {
        PlayerCollision.EventPlayerDamage -= PlayAttack;
    }

    public void Damage(int weaponDamage) //si paso por ahora el index del arma podria hacer variar el stunned provocado por cada una []
    {
        if (health - weaponDamage > 0)
        {
            health -= weaponDamage;

            //if (health <= 80 && health > 60) creatureDamage1.Play(); //este calculo deberia trabajar sobre porcentaje del total de vida, y ademas, hacerse antes de la resta de vida []
            //if (health <= 60 && health > 40) creatureDamage2.Play();
            //if (health <= 40 && health > 20) creatureDamage3.Play();
            creatureDamage2.Play();

            Debug.Log("ENEMY HITTED, hp: " + health);

            //ya que no consegui anim aplico un stunned
            creatureAnimator.SetBool("Walking", false);
            gameObject.GetComponent<Enemy>().run_speed = 0f; //aca deberia usar corrutinas? []
            Invoke("StunnedReset", stunnedTime); // claro estaria bueno que la velocidad haga un fade y no sea instantaneo el cambio []
        }
        else if (health - weaponDamage <= 0)
        {
            creatureDeath.Play();
            Kill();
        }
    }

    void StunnedReset()
    {
        creatureAnimator.SetBool("Walking", true);
        gameObject.GetComponent<Enemy>().run_speed = regularRunSpeed;
    }

    void Kill()
    {
        Debug.Log("ENEMY KILLED");
        //!! matar con timer invoke por anims y sonido, recordar que usar eventos de unity para muchos efectos se facilita []
        //!! ANIM & SOUND HERE
        creatureAnimator.SetTrigger("Death");
        gameObject.GetComponent<Enemy>().enabled = false; // APAGAR Enemy component en vez de matar GO
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Invoke("BodyRemoval", bodyRemovalTime);
        enemyDeath.Invoke();
    }

    void BodyRemoval()
    {
        gameObject.SetActive(false); // aca iria un dissolve effect []
    }

    public int GetHealthPoints() //deberia ser propiedad []
    {
        return health;
    }

    void PlayAttack()
    {
        creatureAttack.Play();
    }
}
