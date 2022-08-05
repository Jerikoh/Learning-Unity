using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 3f;
    public string movementDir = "null";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(movementDir);
    }
    void MoveTo(string value)
    {
        switch (value)
        {
            case "forward":
                transform.position += Vector3.forward * Time.deltaTime * speed;
                break;
            case "back":
                transform.position += Vector3.back * Time.deltaTime * speed;
                break;
            case "right":
                transform.position += Vector3.right * Time.deltaTime * speed;
                break;
            case "left":
                transform.position += Vector3.left * Time.deltaTime * speed;
                break;
            case "up":
                transform.position += Vector3.up * Time.deltaTime * speed;
                break;
            case "down":
                transform.position += Vector3.down * Time.deltaTime * speed;
                break;
            default:
                Debug.Log("La dirección de movimiento especificada en el Inspector no coincide con las opciones disponibles, el GameObject se mantendrá inmovil hasta entonces.");
                break;
        }
    }
}
