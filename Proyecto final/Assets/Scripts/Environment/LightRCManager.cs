using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightRCManager : MonoBehaviour
{
    [SerializeField] Transform originPoint1, endPoint1, originPoint2, endPoint2, originPoint3, endPoint3, originPoint4, endPoint4, originPoint5, endPoint5, lightPosition1, lightPosition2, lightPosition3, lightPosition4, lightPosition5;
    [SerializeField] UIFadeInOut textDialog;
    [SerializeField] AudioSource audioAdvert;
    [SerializeField] AudioSource audioRegularBG;
    [SerializeField] AudioSource audioFightBG;
    [SerializeField] AudioSource audioPostBG;
    Transform actualOriginPoint, actualEndPoint, actualPosition;
    bool flag1 = true;
    bool flag2 = false;
    bool flag3 = false;
    bool flag4 = true; //antenti a esto por si quiero que siga condicional, normalmente false, ver codigo al final
    bool flag5 = false;
    bool allDead = false;
    int kills = 0;
    [SerializeField] GameObject[] toEnable;
    [SerializeField] GameObject[] toDisable;
    public static event Action EventEnding;

    void Start() //para evitar esto podria setearlo arriba del llamado a teleportlights en cada instancia
    {
        EnemyManager.enemyDeath += KillCounter;

        actualOriginPoint = originPoint1;
        actualEndPoint = endPoint1;
        actualPosition = lightPosition1;
    }
    void Update()
    {
        RaycastReader();
    }

    void FixedUpdate()
    {
        if (flag5 && audioRegularBG.volume > 0f)
        {
            audioRegularBG.volume -= Time.fixedDeltaTime * 0.1f; //podria ser editable []
        }

        if (allDead && audioFightBG.volume > 0f)
        {
            audioFightBG.volume -= Time.fixedDeltaTime * 0.03f; //podria ser editable []
        }

        if (allDead && flag5 && audioPostBG.volume < 0.55f && audioFightBG.volume <= 0.1f)
        {
            if (!audioPostBG.isPlaying) audioPostBG.Play();
            audioPostBG.volume += Time.fixedDeltaTime * 0.03f; //podria ser editable []
        }

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
        if (Physics.Linecast(originPoint4.position, endPoint4.position) && flag4) //ver codigo comentado al final
        {
            flag1 = false;
            flag2 = false;
            flag3 = false;
            flag4 = false;
            flag5 = true;
            audioAdvert.volume = 0f;
            TeleportLights(originPoint5, endPoint5, lightPosition5);
            Debug.Log("lightPosition4");
            audioFightBG.Play();

            foreach (GameObject index in toEnable)
            {
                index.SetActive(true);
            }
            foreach (GameObject index in toDisable)
            {
                index.SetActive(false);
            }
        }
        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag5)
        {
            flag5 = false;
            EventEnding?.Invoke();
            /*textDialog.Write("//DEV: Oh, si has llegado hasta aquí ya no hay mucho que ver! Las criaturas rondantes no me dejaban terminar este nivel, gracias por limpiar la zona!", 12f); //!!
            Invoke("SceneLoader", 12f);*/
        }
    }

    /*void SceneLoader()
    {
        SceneManager.LoadScene("MainMenu");
    }*/

    void TeleportLights(Transform originPoint, Transform endPoint, Transform lightPosition)
    {
        audioAdvert.pitch = UnityEngine.Random.Range(0.75f, 1f);
        audioAdvert.Play();

        actualOriginPoint = originPoint;
        actualEndPoint = endPoint;
        actualPosition = lightPosition; //claro esto debería implementarlo en una lista que compare y adelante

        gameObject.transform.position = actualPosition.position;
    }

    void KillCounter()
    {
        kills++;
        Debug.Log("Kill registrada: " + kills);
        if (kills == 5)
        {
            allDead = true; //deberia reemplazar el 5 por un totalEnemies o al menos hacerlo editable []
            Debug.Log("ALL DEAD");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPoint1.position, endPoint1.position); //para raycasts uso drawray
        Gizmos.DrawLine(originPoint2.position, endPoint2.position);
        Gizmos.DrawLine(originPoint3.position, endPoint3.position);
        Gizmos.DrawLine(originPoint4.position, endPoint4.position);
        Gizmos.DrawLine(originPoint5.position, endPoint5.position);
    }

    void OnDisable()
    {
        EnemyManager.enemyDeath -= KillCounter;
    }

    //estaria bueno un plugin que me permita crear accesos directos a X GO en la escena para no estar navegando en la jerarquia 
    //tema aparte, estaría re bueno que los assets sean accedidos en base a content-addressing, para poder organizarlos en multiples folders y para que no refactorice todo cada vez que moves algo
}









/* PIEZA VIEJA por si hago lo de la llave plateada (llave de plata para desbloquear asotea)

        if (Physics.Linecast(actualOriginPoint.position, actualEndPoint.position) && flag4)
        {
            flag4 = false;
            flag5 = true;
            audioAdvert.volume = 0f;
            TeleportLights(originPoint5, endPoint5, lightPosition5);
            Debug.Log("lightPosition4");
            audioFightBG.Play();

            foreach (GameObject index in toEnable)
            {
                index.SetActive(true);
            }
            foreach (GameObject index in toDisable)
            {
                index.SetActive(false);
            }
        }

*/