using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //si no uso nada de monobehaviour, quito la herencia y quiza tenga que poner static a la clase tambien, pero si usa el start para asegurar el singleton... al menos no con los singleton
{
    //!! Todo esto debe ser reescrito! []

    private static int health, maxHealth = 100, keys; //keys deberia desaparecer, por tema dicho abajo, hay una llave para cada puerta o cosa
    private static float energy, maxEnergy = 10f; //si quiero hacer maxX editables, y aun asi poder usar sus valores desde scrit HUD sin inicializar, deberia usar un scriptableobject? []
    private static bool[] key; //luego ver tema de arma actual, armas en posesion, ammo de c/u, otros items []
    public static GameManager instance; //esta var es para asegurarme de que no existe otro elemento GM activo, para lograr el singleton
    [SerializeField][Range(0.01f, 1f)] float energyDrainSpeed = 0.1f; //en realidad debo manejar el valor tras .Radius por completo aca, OJO (.Radius = energy), implementar bien cuando ponga pickups de energy; ya se siente que se van desparramando las funciones sobre variables/propiedades, ej este drain deberia ir en el manejo del scan del player?
    [SerializeField][Range(1, 200)] int healthStart = 100;
    [SerializeField][Range(0f, 10f)] float energyStart = 6;
    [SerializeField] GameObject[] pulseGameObjects; //*esto es por un comportamiento desconocido de los shaders, preventivo, [] pero podria implementar que justo antes de despawnear un item se active el GO placeholder del pulso
    [SerializeField] float SceneReloadDelay = 5f; //[Range(0f, 10f)]

    private void Awake() //para el singleton
    {
        if (instance == null)
        {
            instance = this; //acá relleno la variable con este mismo objeto, para declarar que uno ya existe en la escena

            //acá podría setear los valores que quiera entre cada cambio de escena, ej resetear un score, la vida, etc (o podria hacerlo en otro componente que varie por nivel)

            //DontDestroyOnLoad(gameObject); //con esto garantizo que este GO persista entre escenas con sus valores actuales, esto completa las dos caracteristicas de un singleton; DESACTIVADO POR ADVERTENCIA A VER []
        }
        else
        {
            Destroy(gameObject); //si ya existe uno, se destruye este objeto, para evitar clones
        }
    }

    void Start()
    {
        //energy = PostProcessingScanOriginPlayer.Radius; //estaba en el update, podria ser al reves [] para definirse aca el "energyStart" o startingEnergy (cambiar la var para healthStart)
        health = healthStart;
        energy = energyStart;
        PostProcessingScanOriginPlayer.Radius = energy;
        PulseGORestart(); //*

        PlayerCollision.EventPlayerDeath += OnPlayerDeath;
    }

    void Update()
    {
        if (energy >= 1)
        {
            energy -= Time.deltaTime * energyDrainSpeed;
            PostProcessingScanOriginPlayer.Radius = energy;
        }
    }

    void PulseGORestart() //*
    {
        foreach (GameObject item in pulseGameObjects)
        {
            item.SetActive(false);
            item.SetActive(true);
        }
    }

    void OnPlayerDeath()
    {
        Invoke("ReloadScene", SceneReloadDelay);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDisable()
    {
        PlayerCollision.EventPlayerDeath -= OnPlayerDeath;
    }

    //PROPIEDADES, para acceder a las variables de forma segura (sigo sin entender del todo de que forma la podes arruinar)
    public static int Health { get => health; set => health = value; }
    public static float Energy { get => energy; set => energy = value; }
    public static bool[] Key { get => Key; set => Key = value; }
    public static int Keys { get => keys; set => keys = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
}
