using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManual : MonoBehaviour
{
    [SerializeField]  GameObject projectilePrefab;
    [SerializeField]  float specShootInterval = 0.2f;
    [SerializeField]  float cooldown = 0.5f;
    [SerializeField]  float cooldownSpec = 1.2f;
    bool canShoot = true;
    bool canShootSpec = true;
    int shootRepetitions;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("ShootProjectile", shootDelay, shootInterval);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false; // [] puedo meter esto, el condicional y el reset en los metodos de disparo directamente?
            Invoke("ShootProjectile", 0);
            Invoke("ResetShoot", cooldown);
        }
        if (Input.GetMouseButton(1) && canShootSpec)
        {
            canShootSpec = false;
            SpecialShoot(3);
            Invoke("ResetShootSpec", cooldownSpec);
        }
        if (Input.GetMouseButton(3) && canShootSpec)
        {
            cooldownSpec += 0.5f;
            specShootInterval -= 0.05f;

            canShootSpec = false;
            SpecialShoot(5);
            Invoke("ResetShootSpec", cooldownSpec);

            cooldownSpec -= 0.5f;
            specShootInterval += 0.05f;
        }
        if (Input.GetKey(KeyCode.X) && canShootSpec)
        {
            cooldownSpec += 1;
            specShootInterval -= 0.1f;

            canShootSpec = false;
            SpecialShoot(10);
            Invoke("ResetShootSpec", cooldownSpec);

            cooldownSpec -= 1f;
            specShootInterval += 0.1f;
        }
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation); //podemos tomar la rotacion del prefab o del padre, al caso el del padre cannon porque ahora si me interesa la rotacion
    }

    void ShootSpec()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        if (--shootRepetitions == 0) CancelInvoke("ShootSpec");
    }

    void SpecialShoot(int times) // [] no pude evitar que me largue todo junto con invoke y for, hay una forma mas efectiva para hacer esto? como un "repetir cada X tiempo Y veces"
    {
        shootRepetitions = times;
        InvokeRepeating("ShootSpec", 0f, specShootInterval);
    }

    void ResetShoot()
    {
        canShoot = true;
    }
    void ResetShootSpec() //unificaria los metodos de no ser por el Invoke que no permite parametros?
    {
        canShootSpec = true;
    }


    //estaria bueno poder definir desde el cañon las variables del proyectil que dispara, mas adelante
}
