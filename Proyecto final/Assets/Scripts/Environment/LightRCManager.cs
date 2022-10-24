using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRCManager : MonoBehaviour
{
    [SerializeField] Transform originPoint1, endPoint1, originPoint2, endPoint2, originPoint3, endPoint3, originPoint4, endPoint4, lightPosition1, lightPosition2, lightPosition3, lightPosition4;
    [SerializeField] UIFadeInOut textDialog;
    Transform actualOriginPoint, actualEndPoint, actualPosition;
    bool flag1 = true;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = false;

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
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag1)
        {
            flag1 = false;
            flag2 = true;
            TeleportLights(originPoint2, endPoint2, lightPosition2);
            Debug.Log("lightPosition2");
        }
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag2)
        {
            flag2 = false;
            flag3 = true;
            TeleportLights(originPoint3, endPoint3, lightPosition3);
            Debug.Log("lightPosition3");
        }
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag3)
        {
            flag3 = false;
            flag4 = true;
            TeleportLights(originPoint4, endPoint4, lightPosition4);
            Debug.Log("lightPosition4");
        }
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag4)
        {
            flag4 = false;
            textDialog.Write("//DEV: Oh, si has llegado hasta aquí ya no hay mucho que ver! Las criaturas rondantes no me dejaban terminar este nivel, gracias por limpiar la zona!", 20f); //!!
        }
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
