using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //si no uso nada de monobehaviour, quito la herencia y quiza tenga que poner static a la clase tambien, pero si usa el start para asegurar el singleton... al menos no con los singleton
{
    //static int hp; //variable estatica publica! para acceder desde "cualquier lado" de forma directa, sin traer ningun objeto al script; estas no son visibles en el inspector
    private static int hp; //peero, para evitar posibles "problemas de arquitectura", accederemeos a esta a traves de una propiedad
    private static int energy;
    private static int ammo;
    private static int mags;
    private static int flares;
    private static bool[] keys; //luego ver tema de arma actual, armas en posesion, ammo de c/u, otros items
    public static GameManager instance; //esta var es para asegurarme de que no existe otro elemento GM activo, para lograr el singleton

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //acá relleno la variable con este mismo objeto, para declarar que uno ya existe en la escena

            //acá podría setear los valores que quiera entre cada cambio de escena, ej resetear un score, la vida, etc (o podria hacerlo en otro componente que varie por nivel)

            DontDestroyOnLoad(gameObject); //con esto garantizo que este GO persista entre escenas con sus valores actuales, esto completa las dos caracteristicas de un singleton
        }
        else
        {
            Destroy(gameObject); //si ya existe uno, se destruye este objeto, para evitar clones
        }
    }

    //PROPIEDADES, para acceder a las variables de forma segura (sigo sin entender del todo de que forma la podes arruinar)
    public static int Hp { get => hp; set => hp = value; }
    public static int Energy { get => energy; set => energy = value; }
    public static int Ammo { get => ammo; set => ammo = value; }
    public static int Mags { get => mags; set => mags = value; }
    public static bool[] Keys { get => keys; set => keys = value; }
    public static int Flares { get => flares; set => flares = value; }
}
