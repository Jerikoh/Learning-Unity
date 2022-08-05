using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAuto : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootDelay = 1f;
    public float shootInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", shootDelay, shootInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }

    //estaria bueno poder definir desde el cañon las variables del proyectil que dispara, mas adelante
}
