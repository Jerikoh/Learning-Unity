using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projSpeed = 8;
    //[SerializeField] int projDmg = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProjMovement(projSpeed);

        if (Input.GetKey(KeyCode.X))
        {
            transform.localScale += transform.localScale * 0.05f;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.localScale -= transform.localScale * 0.05f;
        }
    }

    void ProjMovement(float valueSpeed)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * valueSpeed);
    }
}