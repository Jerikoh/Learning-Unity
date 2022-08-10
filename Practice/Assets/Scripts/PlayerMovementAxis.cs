using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAxis : MonoBehaviour
{
    [SerializeField] float movSpeed = 3f;
    Vector3 movDir = new Vector3(0f, 0f, 0f);
    float ejeHorizontal, ejeVertical, mouseX;
    [SerializeField][Range(1f, 5f)] float rotSensitivity = 2f;
    [SerializeField][Range(1f, 10f)] float rotSmoothness = 7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MoveAxis();
    }

    void MoveAxis()
    {
        ejeHorizontal = Input.GetAxisRaw("Horizontal");
        ejeVertical = Input.GetAxisRaw("Vertical");
        movDir = new Vector3(ejeHorizontal, 0, ejeVertical);
        transform.Translate(movDir * movSpeed * Time.deltaTime);
    }

    public void RotatePlayer()
    {
        mouseX += Input.GetAxis("Mouse X"); //hago que cada vez que el player mueva ese imput se cambie ese valor
        //transform.rotation = Quaternion.Euler(0, mouseX * rotSensitivity ,0); //me permite crear un quaternion en relacion a lo que le ponga, pero puedo crear un clase quaternion si me interesan los otros ejes, y estoy sumando porque quiero conservar la rotacion que ya hice antes
        Quaternion targetRotation = Quaternion.Euler(0, mouseX * rotSensitivity, 0); //supuestamente este suavizado de rotSens no es necesario y basta con el rotSmo, probar luego
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotSmoothness * Time.deltaTime); //implementado lerp
    }
}
