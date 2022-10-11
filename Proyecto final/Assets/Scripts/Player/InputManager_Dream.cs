using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager_Dream : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Animator playerAnimator; //si me organizo mejor , esto podria ser un manager []

    //variables readonly que informan al PlayerController el status de nuestros input
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    //ref o hook de cada tipo, para acceder al input map y a cada input action individual
    InputActionMap currentMap;
    //
    InputAction moveAction;
    InputAction lookAction;
    InputAction runAction;
 
    void Awake()
    {
        HideCursor();

        //aca obtenemos nuestro InputActionMap y actions particulares, seteando refs, para poder manipularlos localmente
        currentMap = playerInput.currentActionMap;
        //
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");

        //aca suscribimos cada funcion callback a cada input action, hacemos que cuando sucede tal accion suceda el metodo especificado, como cualquier evento
        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;

        //performed es cuando el keybinding empieza y canceled cuando termina, tambien esta waiting, started y disabled (y otras mas generales)
        moveAction.canceled += onMove;
        lookAction.canceled += onLook;
        runAction.canceled += onRun;

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

    //para "organizar" el proyecto y evitar problemas de arquitectura
    void OnDisable()
    {
        currentMap.Disable();
    }
}
