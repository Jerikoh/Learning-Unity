using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Animator playerAnimator; //si me organizo mejor , esto podria ser un manager []

    //EVENTS
    public static event Action EventPlayerTrigger;
    public static event Action EventPlayerReload;
    public static event Action EventPlayerWalk;
    public static event Action EventPlayerRun;
    public static event Action EventPlayerSlash;
    public static event Action EventPlayerPush;

    //onShoot, onThrow, onMelee, onUse;

    //variables readonly que informan al PlayerController el status de nuestros input
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }
    public bool Use { get; private set; }
    public bool Equip1 { get; private set; }
    public bool Equip2 { get; private set; }
    public bool Equip3 { get; private set; }
    public bool Equip4 { get; private set; }
    public bool Pause { get; private set; }

    //ref o hook de cada tipo, para acceder al input map y a cada input action individual
    InputActionMap currentMap;
    //
    InputAction moveAction;
    InputAction lookAction;
    InputAction runAction;
    InputAction useAction, shootAction, reloadAction, slashAction, equip1Action, equip2Action, equip3Action, equip4Action, pauseAction;

    //para acceder al script del GO WeaponManager, siento que no es muy organizado conectarlo al inputmanager
    [SerializeField] GameObject weaponManagerGO;

    //para el camera bobbing, ante la ausencia o mi ignorancia de un "while pressed" o "hold" action []
    bool walking = false;
    bool running = false;
    //chanchada para habilitar melee, deberia ir al menos en weaponmanager []
    bool delayReady = true;

    void Awake()
    {
        HideCursor();

        //aca obtenemos nuestro InputActionMap y actions particulares, seteando refs, para poder manipularlos localmente
        currentMap = playerInput.currentActionMap;
        //
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");
        useAction = currentMap.FindAction("Use");
        shootAction = currentMap.FindAction("Shoot");
        reloadAction = currentMap.FindAction("Reload");
        slashAction = currentMap.FindAction("Melee");
        equip1Action = currentMap.FindAction("Equip weapon 1");
        equip2Action = currentMap.FindAction("Equip weapon 2");
        equip3Action = currentMap.FindAction("Equip weapon 3");
        equip4Action = currentMap.FindAction("Equip weapon 4");
        pauseAction = currentMap.FindAction("Pause");

        //aca suscribimos cada funcion callback a cada input action, hacemos que cuando sucede tal accion suceda el metodo especificado, como cualquier evento
        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;
        useAction.performed += onUse;
        shootAction.performed += onShoot;
        reloadAction.performed += onReload;
        slashAction.performed += onSlash;
        equip1Action.performed += onEquip1;
        equip2Action.performed += onEquip2;
        equip3Action.performed += onEquip3;
        equip4Action.performed += onEquip4;
        pauseAction.performed += onPause;

        //performed es cuando el keybinding empieza y canceled cuando termina, tambien esta waiting, started y disabled (y otras mas generales)
        moveAction.canceled += onMove;
        lookAction.canceled += onLook;
        runAction.canceled += onRun;
        //useAction.canceled += onUse;
        //shootAction.canceled += onShoot;
        //slashAction.canceled += onSlash;
        //equip1Action.canceled += onEquip1;
        //equip2Action.canceled += onEquip2;
        //equip3Action.canceled += onEquip3;
        //equip4Action.canceled += onEquip4;

        //para el camera bobbing, no es lo mejor seguramente []
        moveAction.performed += isWalking;
        moveAction.canceled += isNotWalking;
        runAction.performed += isRunning;
        runAction.canceled += isNotRunning;
        shootAction.canceled += noMoreShooting;
    }

    void Update()
    {
        //para el camera bobbing
        if (walking)
        {
            EventPlayerWalk?.Invoke();
        }
        if (running)
        {
            EventPlayerRun?.Invoke();
        }
    }

    //para el camera bobbing
    void isWalking(InputAction.CallbackContext context)
    {
        walking = true;
    }
    void isNotWalking(InputAction.CallbackContext context)
    {
        walking = false;
    }
    void isRunning(InputAction.CallbackContext context)
    {
        running = true;
    }
    void isNotRunning(InputAction.CallbackContext context)
    {
        running = false;
    }
    void noMoreShooting(InputAction.CallbackContext context)
    {
        //!! ACA DEBERIA REACTIVAR COMPONENTE ALT3?
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //aca creamos cada funcion callback a la que asociamos cada input action
    void onMove(InputAction.CallbackContext context) //cada una tiene sus propios parametros callback [] profundizar
    {
        Move = context.ReadValue<Vector2>();
    }

    void onLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    void onRun(InputAction.CallbackContext context)
    {
        Run = context.ReadValueAsButton();
    }

    void onUse(InputAction.CallbackContext context)
    {
        //playerAnimator.SetTrigger("Pick-up"); //!! deberia efectuarse desde el raycaster, de disponerse un elemento interactuable y efectuar el jugador la interaccion []
        //weaponManagerGO.GetComponent<WeaponManager>().CheckAmmo(); // [] mientras hacer que sea el medio para consultar ammo del arma actual, o podria crear un input dedicado con Q y una animacion que por ahora seria la de recarga
    }

    void onShoot(InputAction.CallbackContext context) // [] deberia ser onAttack, que invoke un evento PlayerAttack, que procese el efecto segun lo que tenga equipado
    {
        if (!WeaponManager.PlayerCanMove) return;

        //!! PARA TEMA CAM SHAKE; ANTES DE INVOCAR ESTE EVENTO DEBERIA APAGAR EL COMPONENTE SHAKE ALT3 Y AL TERMINAR EL DISPARO REACTIVARLO, same para DMG
        //esto es para el overlap entre camera shakes scripts, para solucionar deberia unificar un valor y que cada uno procese el total en sus propios terminos []

        if (WeaponManager.EquippedItem == 3) //!! [] +++ si hiciera una evaluacion aca, solo comprobaria si se trata de firegun o melee como para distinguir un evento trigger (gatillo y otro slash -distinto al onSlash usado mas abajo que corresponde a empujon-)
        {
            EventPlayerTrigger?.Invoke();
        }
        //again, onShoot deberia ser onPlayerAttack
        if (WeaponManager.EquippedItem == 2 && delayReady)
        {
            delayReady = false;
            Invoke("DelayReady", 0.4f); //tambien deberia poder controlarse desde el editor []
            EventPlayerSlash?.Invoke();
            playerAnimator.SetTrigger("Axe slash"); //!! temporal para darse una idea de la anim []
        }
    }

    void DelayReady()
    {
        delayReady = true;
    }

    void onReload(InputAction.CallbackContext context)
    {
        if (!WeaponManager.PlayerCanMove) return;

        if (WeaponManager.EquippedItem == 3) //!!
        {
            EventPlayerReload?.Invoke();
        }
    }

    void onSlash(InputAction.CallbackContext context)
    {
        if (!WeaponManager.PlayerCanMove) return;
        if (!WeaponManager.PlayerReloadReady) return;

        //llamar EVENTO de melee [] este con click derecho en realidad es un empujon para alejar o romper, y todas las armas lo tienen, el click izquierdo con armas melee es el que provoca mas da??o (esto deberia ser OnSlash)
        //tambien agregar el delay habilitador desde weaponmanager, daria que tenga stamina tambien []
        if ((WeaponManager.EquippedItem == 3 || WeaponManager.EquippedItem == 2) && delayReady)
        {
            delayReady = false;
            Invoke("DelayReady", 0.3f); //tambien deberia poder controlarse desde el editor []
            EventPlayerPush?.Invoke();
            playerAnimator.Play("pistol whip"); //anim que deberia ser relativa al arma equipada con cual se golpea [] deberia haber un AnimManager escuchando y no efectuarse ac??, asi como un EventManager al cual poder llamar desde cualquier lado
        }
    }

    /* ESTAS SON LAS CORRECTAS, por ahora se aplic?? esto ante no poder usar el scroll del mouse (no sabr??a por que no aparece en la lista de inputs)
    void onEquip1(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon1) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(0);
    }

    void onEquip2(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon2) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(1);
    }
    */
    void onEquip1(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon4) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(3);
    }

    void onEquip2(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon3) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(2);
    }

    void onEquip3(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon3) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(2);
    }

    void onEquip4(InputAction.CallbackContext context)
    {
        if (!WeaponManager.Weapon4) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(3);
    }

    void onPause(InputAction.CallbackContext context)
    {
        //aca deberia invocar evento escuchado por un deathmanager, en vez de repetir todo ya 3 veces []
        GameManager.Health = 0;
        WeaponManager.Weapon1 = false;
        WeaponManager.Weapon2 = false;
        WeaponManager.Weapon3 = false;
        WeaponManager.Weapon4 = false;
        WeaponManager.EquippedItem = 5;
        WeaponManager.Weapon4_Ammo = 0;
        WeaponManager.Weapon4_AmmoIn = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //para evitar posibles problemas
    void OnDisable()
    {
        currentMap.Disable();
    }
}
