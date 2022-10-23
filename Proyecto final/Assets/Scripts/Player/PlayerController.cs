using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float animBlendSpeed = 9f;
    Rigidbody playerRigidbody;
    InputManager inputManager;

    //para acceder a nuestra animation stuff
    Animator animator;
    bool hasAnimator;

    //las propiedades que seteamos en nuestro animador, necesitamos sus hashs
    int xVelHash;
    int yVelHash;

    //las velocidades que definimos en el blend tree, en AnimPlayerController
    const float walkSpeed = 3f;
    const float runSpeed = 5f;

    //la representacion 2d de nuestra velocidad
    Vector2 currentVelocity;

    //camera thingz [] dps ver si se puede lograr lo mismo usando cinemachine
    [SerializeField] Transform cameraTarget;
    [SerializeField] new Transform camera; //acá agregue el "new" sugerido por advertencia, desactivar ante unexpected errors
    [SerializeField] float upperLimit = -40f;
    [SerializeField] float bottomLimit = 70f;
    [SerializeField] float mouseSensitivity = 20f;
    float xRotation;

    //para que al inicio de la escena no se mueva**, igual no corresponde ponerlo en el playercontroller sino en el scenemanager, pero tambien deberia suceder condicionalmente la animacion de entrada []
    bool canMove = false;

    //para acceder al script del GO WeaponManager
    //[SerializeField] GameObject weaponManagerGO;

    void Start()
    {
        //inicializando los componentes y variables...
        hasAnimator = TryGetComponent<Animator>(out animator);
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();

        xVelHash = Animator.StringToHash("xVelocity");
        yVelHash = Animator.StringToHash("yVelocity");

        Invoke("CanMove", 5f);
    }

    void CanMove()
    {
        canMove = true;
    }

    void FixedUpdate() //ya que estamos usando rigidbody
    {
        if (canMove) Move();
    }

    void LateUpdate()
    {
        CameraMovement();
    }

    void Move()
    {
        if (!hasAnimator) return; //comprobamos que el player tenga un Animator, sino la funcion Move no se ejecuta

        //definimos target speed segun cada caso
        float targetSpeed = inputManager.Run ? runSpeed : walkSpeed;
        if (inputManager.Move == Vector2.zero) targetSpeed = 0.1f;

        //aca definimos nuestra currentVelocity en tanto la targetSpeed
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, animBlendSpeed * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, animBlendSpeed * Time.fixedDeltaTime);

        //esto es para que el rigidbody no acelere infinitamente (?)
        var xVelDifference = currentVelocity.x - playerRigidbody.velocity.x;
        var zVelDifference = currentVelocity.y - playerRigidbody.velocity.z;

        //ahora definimos la velocidad del rigidbody en tanto nuestra currentVelocity
        playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange); //siendo que calculamos en modo local, queremos transferir nuestra velocidad local a global

        //pasamos al Animator nuestra currentVelocity, para animar adecuadamente
        animator.SetFloat(xVelHash, currentVelocity.x);
        animator.SetFloat(yVelHash, currentVelocity.y);
    }

    void CameraMovement()
    {
        if (!hasAnimator) return;

        //aca captamos el input del inputmanager
        var mouseX = inputManager.Look.x;
        var mouseY = inputManager.Look.y;

        camera.position = cameraTarget.position;

        xRotation -= mouseY * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, upperLimit, bottomLimit);

        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, mouseX * mouseSensitivity * Time.deltaTime);
    }
}
