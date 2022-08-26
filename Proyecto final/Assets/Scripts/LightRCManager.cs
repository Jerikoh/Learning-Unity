using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRCManager : MonoBehaviour
{
    [SerializeField] Transform originPoint1, endPoint1, originPoint2, endPoint2, nextLightPosition1, nextLightPosition2;
    Transform actualOriginPoint, actualEndPoint, actualNextPosition;

    void Start()
    {
        actualOriginPoint = originPoint1;
        actualEndPoint = endPoint1;
        actualNextPosition = nextLightPosition1;
    }

    void Update()
    {
        RaycastReader();
    }

    private void RaycastReader()
    {
        //RaycastHit hitted; //no lo uso al caso, hay otro llamado que no lo cuente? LINECAST
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position))
        {
            gameObject.transform.position = actualNextPosition.position;

            actualOriginPoint = originPoint2;
            actualEndPoint = endPoint2;
            actualNextPosition = nextLightPosition2; //claro esto debería implementarlo en una lista que compare y adelante
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(originPoint1.position, endPoint1.position);
        Gizmos.DrawLine(originPoint2.position, endPoint2.position);
    }

    //estaria bueno un plugin que me permita crear accesos directos a X GO en la escena para no estar navegando en la jerarquia 
    //tema aparte, estaría re bueno que los assets sean accedidos en base a content-addressing, para poder organizarlos en multiples folders y para que no refactorice todo cada vez que moves algo
}
