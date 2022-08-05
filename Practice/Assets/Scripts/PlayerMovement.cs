using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movSpeed = 2f;
    public Vector3 movDir = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movDir.Set(0f,0f,1f);
            Move(movSpeed, movDir); //[] no puedo pasar directamente unas coordenadas? o setear el valor del vector tipo movDir = (0f,0f,1f)? 
        }
        if (Input.GetKey(KeyCode.S))
        {
            movDir.Set(0f,0f,-1f);
            Move(movSpeed, movDir);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movDir.Set(1f,0f,0f);
            Move(movSpeed, movDir);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movDir.Set(-1f,0f,0f);
            Move(movSpeed, movDir);
        }
        if (Input.GetKeyDown(KeyCode.Space)) //hacer pseudo jetpack?
        {
            movDir.Set(0f,1f,0f);
            Move(movSpeed, movDir);
        }
    }

    void Move(float refSpeed, Vector3 refDir)
    {
        transform.Translate(refDir * Time.deltaTime * refSpeed);
    }
}
