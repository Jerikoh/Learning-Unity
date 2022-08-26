using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] Transform shootOrigin; //sigo sin saber si es necesario o no poner el private o se toma naturalmente como tal al no poner nada
    [SerializeField][Range(1f, 20f)] float rayDistance; //para determinar la distancia del rayo, de ser necesario
  

    void Update()
    {
        RaycastReader(); //esto se podría llamar al disparar, y claro en el debug quitaría vida al enemy
    }

    private void RaycastReader()
    {
        RaycastHit hitted;
         if (Physics.Raycast(shootOrigin.position, shootOrigin.forward, out hitted, rayDistance)) //
        {
            if (hitted.transform.CompareTag("Enemy")) //pregunto si el tag de lo que chocó es el enemigo
            {
                Debug.Log("ENEMY");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(shootOrigin.position, shootOrigin.forward * rayDistance); //este .forward hace que tome la direccion local y no global, creeria
    }

    //estaria bueno un plugin que me permita crear accesos directos a X GO en la escena para no estar navegando en la jerarquia 
    //tema aparte, estaría re bueno que los assets sean accedidos en base a content-addressing, para poder organizarlos en multiples folders y para que no refactorice todo cada vez que moves algo
}
