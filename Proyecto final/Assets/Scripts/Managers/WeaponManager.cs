using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //!! si con este pretendo almacenar el inventario de armas y munición, deberé convertirlo en un singleton []
    //deberia ser diferente el sistema para indicar posesion y para indicar "posicion" [] para que sea independiente la posicion en el index de la posicion en slots 1-2-3-4 (tales deberian ser por tipo de arma a lo HL); objetos? scriptableobjects? con estado de equipped, ammo, durabilidad, etc*
    //y quiza cada arma "fisica" deberia contener las variables y gestar los eventos y/o metodos

    //cuando son referencias fijas, definimos desde el Inspector, al no ser dinamico o variar luego (?)
    [SerializeField] GameObject[] weapons;
    [SerializeField] Animator playerAnimator; //si me organizo mejor , esto podria ser un manager []
    [SerializeField] Transform playerHand;
    [SerializeField] MeleeCollider revolverCollider; //estos dos debo hacerlos al no saber por que sus GO no escuchan eventos
    [SerializeField] MeleeCollider hatchetCollider;

    private static int equippedItem = 5, flares = 0, lastEquipped; //por ahora el 5 en equippedItem se usa como null, pero no deberia ser asi
    private static bool weapon1, weapon2, weapon3, weapon4;

    bool weaponReady = false; //al ser relativo al arma equipada, es comun a todas (?)
    bool delayReady = true;
    static bool reloadReady = true;
    bool start = true;

    //puntualmente para el revolver, de nuevo no deberia ser así* []
    [SerializeField] WeaponRaycastManager weapon4_GO;
    static int weapon4_AmmoIn = 0; 
    static int weapon4_Ammo = 0; 
    [SerializeField] int weapon4_Rounds = 6;
    [SerializeField] float weapon4_ReloadTime = 4f;
    [SerializeField] float weapon4_FireDelayTime = 1f;
    [SerializeField] int weapon4_Damage = 30;
    [SerializeField] int weapon4_MeleeDamage = 5;

    //EVENTOS
    public static event Action EventPlayerShoot;
    public static event Action EventPlayerReload; //!! por ahora actua puntualmente para el revolver [] **

    [SerializeField] SoundManager soundManagerGO; //tampoco deberia estar aqui []
    [SerializeField] UIFadeInOut textDialog; //tampoco deberia estar aqui []

    //para que al inicio de la escena no intente disparar, igual no correspondería aqui, same thing en playercontroller []
    static bool canMove = false;

    //ammo, mags, flares...

    //ver si mas adelante sigo necesitando estas propiedades, sino eliminarlas [] o al menos los setters
    public static bool Weapon1 { get => weapon1; set => weapon1 = value; }
    public static bool Weapon2 { get => weapon2; set => weapon2 = value; }
    public static bool Weapon3 { get => weapon3; set => weapon3 = value; }
    public static bool Weapon4 { get => weapon4; set => weapon4 = value; }
    public static int EquippedItem { get => equippedItem; set => equippedItem = value; }
    public static bool PlayerCanMove { get => canMove; set => canMove = value; }
    public static bool PlayerReloadReady { get => reloadReady; set => reloadReady = value; }
    public static int LastEquipped { get => lastEquipped; set => lastEquipped = value; }
    public static int Weapon4_Ammo { get => weapon4_Ammo; set => weapon4_Ammo = value; }
    public static int Weapon4_AmmoIn { get => weapon4_AmmoIn; set => weapon4_AmmoIn = value; }

    //public static int Ammo { get => ammo; set => ammo = value; }
    //public static int Mags { get => mags; set => mags = value; }
    //public static int Flares { get => flares; set => flares = value; }

    //----------------------------------------------------------AUXILIAR/TEMPORAL (ante este build improvizado con solo revolver)
    void Start()
    {
        canMove = false;
        EquipWeapon(3); //solo para demostracion[]

        InputManager.EventPlayerTrigger += RevolverShoot;
        InputManager.EventPlayerReload += RevolverReload;
        InputManager.EventPlayerPush += OnPush;
        InputManager.EventPlayerSlash += OnSlash;

        Invoke("CanMove", 5f);
    }

    void OnDisable()
    {
        InputManager.EventPlayerTrigger -= RevolverShoot;
        InputManager.EventPlayerReload -= RevolverReload;
        InputManager.EventPlayerPush -= OnPush;
        InputManager.EventPlayerSlash -= OnSlash;
    }

    void OnPush()
    {
        switch (equippedItem)
        {
            case 2:
                hatchetCollider.DoPush();
                break;
            case 3:
                revolverCollider.DoPush();
                break;
            default:
                Debug.Log("Ningun arma equipada");
                break;
        }
    }

    void OnSlash()
    {
        if (!canMove) return;
        switch (equippedItem)
        {
            case 2:
                hatchetCollider.DoSlash();
                break;
            case 3:
                revolverCollider.DoSlash();
                break;
            default:
                Debug.Log("Ningun arma equipada");
                break;
        }
    }

    void CanMove()
    {
        canMove = true;
    }

    void RevolverShoot()
    {
        if (!canMove) return;
        Shoot(weapon4_AmmoIn, weapon4_Damage, weapon4_FireDelayTime);
    }

    void RevolverReload()
    {
        if (!canMove) return;
        WeaponReload(weapon4_AmmoIn, weapon4_Rounds, weapon4_Ammo, weapon4_ReloadTime);
    }
    //----------------------------------------------------------

    void ReloadReady()
    {
        reloadReady = true;
        Debug.Log("ARMA RECARGADA, ammoIn: " + weapon4_AmmoIn + ", totalammo: " + weapon4_Ammo);
        //en realidad deberia hacer un return y no trabajar con una variable comun a todas las funciones, para que se actualice al momento de la consulta y no en el update? y lo mismo para los demas? aunque podria hacer  esa actualizacion llamando al metodo en shoot o donde se requiera un estado actualizado del booleano
    }

    void WeaponReload(int ammoIn, int weaponRounds, int ammo, float reloadTime)
    {
        if (reloadReady && ammoIn < weaponRounds && ammo > 0) // esto convendria checkearlo en el reseteador?
        {
            reloadReady = false;
            Invoke("ReloadReady", reloadTime);
            ammo += ammoIn;
            ammoIn = 0;
            Debug.Log("Recargando...");
            EventPlayerReload?.Invoke(); //!! **
            playerAnimator.SetTrigger("Pistol reload"); //!! lo mismo, es una anim propia del revolver y no comun a toda arma de fuego
            while (ammoIn < weaponRounds && ammo > 0) //seguro haya otra forma para recargar "progresivamente"
            {
                ammo--;
                ammoIn++;
            }
            //para esta build donde uso solo el revolver; aca supongo deberia trabajar con objetos... o se pueden escribir desde aca las variables que se le pasaron como referencia? para manipular directamente [] ++ buscar por que no hacen funciones con mas de una salida o producto indicable como salida
            //EN REALIDAD si tuviese una lista para cada variable de arma y todas estas compartieran el mismo indice, podria evitar todo esto al pasar el indice como parametro, ej weaponAmmo[3] (municion de arma numero 3) [] +++
            weapon4_Ammo = ammo;
            weapon4_AmmoIn = ammoIn;
        }
        else if (!reloadReady) Debug.Log("Estoy en eso");
        else if (ammoIn >= weaponRounds) textDialog.Write("El arma está cargada", 2f);
        else if (ammo <= 0) textDialog.Write("No tengo munición", 2f);
    }

    void DelayReady()
    {
        delayReady = true;
    }

    void Shoot(int ammoIn, int weaponDamage, float delayTime)
    {
        if (delayReady && reloadReady) //mas adelante, if weaponequipped es firegun? +++ []
        {
            soundManagerGO.PlayTrigger(); //!! again solo al caso del revolver, el sonido de pull trigger (deberia haber uno propio para cada arma)
            if (ammoIn > 0)
            {
                ammoIn--;
                weapon4_AmmoIn = ammoIn;
                delayReady = false;
                Invoke("DelayReady", delayTime);
                EventPlayerShoot?.Invoke(); //para que escuchen shake, sonidos, anims, etc +++ [] 
                Debug.Log("DISPARO EFECTUADO, ammoIn: " + weapon4_AmmoIn + ", totalammo: " + weapon4_Ammo);
                weapon4_GO.RaycastReader(weaponDamage);
                //!! LLAMAR METODO DE RAYCAST CHECKER, o invokar evento de raycast check, y pasar variable daño (vía sin proyectiles), si este da positivo este efectuara el daño en el "other"
                playerAnimator.SetTrigger("Pistol shooting"); //!! el tema es que es particular al revolver, deberia adaptarse al arma con que se disparó, ante eso en anim manager segun el arma equipada? o cada arma objeto tendria su metodo
            }
            else if (ammoIn <= 0) textDialog.Write("El arma está descargada [R]", 2f);
        }
        else if (!delayReady) Debug.Log("No puedo disparar tan seguido");
        else if (!reloadReady) Debug.Log("Aún no terminé de recargar");
    }

    void DisableWeapons()
    {
        foreach (GameObject index in weapons)
        {
            index.SetActive(false);
        }
    }

    public void EquipWeapon(int index) //esto se llama desde el PLAYERCOLLISION al triggear el item, el metodo es public para que eso sea posible, pero estaria que sea evento + index []
    {
        if (index != equippedItem)
        {
            //desactivamos todas las armas para que no se superpongan; [] CONSIDERAR el tema de la conservacion de armas via GAMEMANAGER
            DisableWeapons();
            if (!start) playerAnimator.Play("Pistol draw"); //!! podria ser mucho mejor [] al menos darle un delay al destroy/equip como para sumarle al efecto de recoger
            if (start) start = false; //esto es un parche chancho para evitar la anim al levantarse, en otra situacion deberia irse
            //activamos la primer arma
            weapons[index].SetActive(true);
            soundManagerGO.PlayEquip();
            //indico el arma equipada actualmente, podria ser un evento anunciando el arma actual []
            lastEquipped = equippedItem;
            equippedItem = index;

            //agrego el arma a la palma del player
            weapons[index].transform.parent = playerHand; //reparenting!

            //para ajustar la posicion del objeto en relacion al nuevo padre, ahora local
            weapons[index].transform.localPosition = Vector3.zero;
            weapons[index].transform.localScale = new Vector3(1f, 1f, 1f); //correccion por redimension de player creo?
            weapons[index].transform.localRotation = Quaternion.Euler(0, 0, 0); //same

            //temporal, deberia ser diferente usando objetos u otra solucion* 
            switch (index)
            {
                case 0:
                    weapon1 = true;
                    break;
                case 1:
                    weapon2 = true;
                    break;
                case 2:
                    weapon3 = true;
                    break;
                case 3:
                    weapon4 = true;
                    break;
            }
        }
    }

    /* public void CheckAmmo() //* [] aca deberia pasar un parametro para consultar especificamente la municion del arma equipada
    {
        //si el arma es revolver
        //"X balas en el tambor, y Y para recargarle"
        string ammoInform = "RevolverAmmo: " + GameManager.PlayerAmmo["RevolverAmmo"] + " | ShotgunAmmo: " + GameManager.PlayerAmmo["ShotgunAmmo"] + " | RifleAmmo: " + GameManager.PlayerAmmo["RifleAmmo"];
        Debug.Log(ammoInform);
    }*/
}
