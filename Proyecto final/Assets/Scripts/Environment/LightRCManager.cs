using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRCManager : MonoBehaviour
{
    [SerializeField] Transform originPoint1, endPoint1, originPoint2, endPoint2, originPoint3, endPoint3, originPoint4, endPoint4, lightPosition1, lightPosition2, lightPosition3, lightPosition4;
    Transform actualOriginPoint, actualEndPoint, actualPosition;
    bool flag1 = true; //esto es trampa
    bool flag2 = true;
    bool flag3 = true;

    void Start() //para evitar esto podria setearlo arriba del llamado a teleportlights en cada instancia
    {
        actualOriginPoint = originPoint1;
        actualEndPoint = endPoint1;
        actualPosition = lightPosition1;
    }
    void Update()
    {
        RaycastReader();
    }

    private void RaycastReader()
    {
        //RaycastHit hitted; //no lo uso al caso, hay otro llamado que no lo cuente? LINECAST; pero NECESITO RAYCAST para hacer  && hitted.transform.CompareTag("Enemy") y que no lo hagan otros []
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag1 && flag2 && flag3)
        {
            flag1 = false;
            TeleportLights(originPoint2, endPoint2, lightPosition2);
            Debug.Log("lightPosition2");
        }
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && !flag1 && flag2 && flag3)
        {
            flag2 = false;
            TeleportLights(originPoint3, endPoint3, lightPosition3);
            Debug.Log("lightPosition3");
        }
       /* if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && !flag1 && !flag2 && flag3) //podria hacer otra para que no repita esto, aunque no se note
        {
            flag3 = false;
            TeleportLights(originPoint4, originPoint4, lightPosition4);
            Debug.Log("lightPosition4");
        }
        */

    }

    void TeleportLights(Transform originPoint, Transform endPoint, Transform lightPosition)
    {
        actualOriginPoint = originPoint;
        actualEndPoint = endPoint;
        actualPosition = lightPosition; //claro esto debería implementarlo en una lista que compare y adelante

        gameObject.transform.position = actualPosition.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPoint1.position, endPoint1.position); //para raycasts uso drawray
        Gizmos.DrawLine(originPoint2.position, endPoint2.position);
        Gizmos.DrawLine(originPoint3.position, endPoint3.position);
        Gizmos.DrawLine(originPoint4.position, endPoint4.position);
    }

    //estaria bueno un plugin que me permita crear accesos directos a X GO en la escena para no estar navegando en la jerarquia 
    //tema aparte, estaría re bueno que los assets sean accedidos en base a content-addressing, para poder organizarlos en multiples folders y para que no refactorice todo cada vez que moves algo
}
