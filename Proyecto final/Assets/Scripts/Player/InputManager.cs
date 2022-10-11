using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Animator playerAnimator; //si me organizo mejor , esto podria ser un manager []

    //EVENTS
    public static event Action EventPlayerShoot;

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

    //ref o hook de cada tipo, para acceder al input map y a cada input action individual
    InputActionMap currentMap;
    //
    InputAction moveAction;
    InputAction lookAction;
    InputAction runAction;
    InputAction useAction, shootAction, slashAction, equip1Action, equip2Action, equip3Action, equip4Action;

    //para acceder al script del GO WeaponManager, siento que no es muy organizado conectarlo al inputmanager
    [SerializeField] GameObject weaponManagerGO;

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
        slashAction = currentMap.FindAction("Melee");
        equip1Action = currentMap.FindAction("Equip weapon 1");
        equip2Action = currentMap.FindAction("Equip weapon 2");
        equip3Action = currentMap.FindAction("Equip weapon 3");
        equip4Action = currentMap.FindAction("Equip weapon 4");

        //aca suscribimos cada funcion callback a cada input action, hacemos que cuando sucede tal accion suceda el metodo especificado, como cualquier evento
        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;
        useAction.performed += onUse;
        shootAction.performed += onShoot;
        slashAction.performed += onSlash;
        equip1Action.performed += onEquip1;
        equip2Action.performed += onEquip2;
        equip3Action.performed += onEquip3;
        equip4Action.performed += onEquip4;

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

    void onUse(InputAction.CallbackContext context) // [] ACTUALMENTE USADO PARA DEMO DICC
    {
        weaponManagerGO.GetComponent<WeaponManager>().CheckAmmo();
    }

    void onShoot(InputAction.CallbackContext context) // [] ACTUALMENTE USADO PARA DEMO DICC
    {
        //llamar EVENTO de disparo, si esta equipada x arma []
        EventPlayerShoot?.Invoke();
        //playerAnimator.Play("pistol shooting"); //esta anim esta mal xd VOLVER A BAJAR Y RIGGEAR []
    }

    void onSlash(InputAction.CallbackContext context) // [] ACTUALMENTE USADO PARA DEMO DICC
    {
        //llamar EVENTO de melee, si esta equipada x arma []
        //EventPlayerSlash?.Invoke();
        playerAnimator.Play("pistol whip"); //anim si esta equipado el revolver []

        //tambien agregar el delay
    }

    void onEquip1(InputAction.CallbackContext context)
    {
        if (!GameManager.Weapon1) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(0);
    }

    void onEquip2(InputAction.CallbackContext context)
    {
        if (!GameManager.Weapon2) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(1);
    }

    void onEquip3(InputAction.CallbackContext context)
    {
        if (!GameManager.Weapon3) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(2);
    }

    void onEquip4(InputAction.CallbackContext context)
    {
        if (!GameManager.Weapon4) return;
        weaponManagerGO.GetComponent<WeaponManager>().EquipWeapon(3);
    }

    //para "organizar" el proyecto y evitar problemas de arquitectura
    void OnDisable()
    {
        currentMap.Disable();
    }
}
