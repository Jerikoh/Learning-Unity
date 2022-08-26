using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRCManager : MonoBehaviour
{
    [SerializeField] Transform originPoint1;
    [SerializeField] Transform endPoint1;
    [SerializeField] Transform originPoint2;
    [SerializeField] Transform endPoint2;
    [SerializeField] Transform nextLightPosition;
    Transform actualOriginPoint, actualEndPoint;

    void Start()
    {
        actualOriginPoint = originPoint1;
        actualEndPoint = endPoint1;
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
            actualOriginPoint = originPoint2;
            actualEndPoint = endPoint2;
            gameObject.transform.position = nextLightPosition.position;
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