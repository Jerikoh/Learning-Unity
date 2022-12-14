using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycastManager : MonoBehaviour
{
    [SerializeField] Transform shootOrigin; //sigo sin saber si es necesario o no poner el private o se toma naturalmente como tal al no poner nada
    [SerializeField][Range(1f, 20f)] float rayDistance; //para determinar la distancia del rayo, de ser necesario

    //propio del nivel particular, no debería estar aqui []
    bool firstContact = false;
    [SerializeField][Range(1f, 10f)] float rayDistContact = 3f; //para determinar la distancia del rayo, de ser necesario
    [SerializeField] SoundManager soundManagerGO; //tampoco deberia estar aqui []

    void Update()
    {
        if (!firstContact)
        {
            RaycastContact();
        }
    }

    public void RaycastReader(int weaponDamage) // [] debería poder pasarle otros parametros para pdoer usarlo con armas melee, como tipo de arma, distancia/alcance, etc (cosas que tambien podria pasarle al bicho desde aca, para que reaccione adecuadamente)
    {
        RaycastHit hitted;
        if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out hitted, rayDistance))
        {
            if (hitted.transform.CompareTag("Enemy")) //pregunto si el tag de lo que chocó es el enemigo
            {
                hitted.transform.gameObject.GetComponent<EnemyManager>().Damage(weaponDamage); // [] claro se podria hacer que pierda velocidad por vida total o temporal por daño reciente, que regenere tras x tiempo, que quede knock out, etc
            }
        }
    }

    public void RaycastContact()
    {
        RaycastHit contacted;
        if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out contacted, rayDistContact))
        {
            if (contacted.transform.CompareTag("Enemy"))
            {
                firstContact = true;
                soundManagerGO.PlayContact();
                Debug.Log("CONTACTED");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootOrigin.position, shootOrigin.forward * rayDistance); //este .forward hace que tome la direccion local y no global
    }

    //estaria bueno un plugin que me permita crear accesos directos a X GO en la escena para no estar navegando en la jerarquia (?)
    //tema aparte, estaría re bueno que los assets sean accedidos en base a content-addressing, para poder organizarlos en multiples folders y para que no refactorice todo cada vez que moves algo
}
