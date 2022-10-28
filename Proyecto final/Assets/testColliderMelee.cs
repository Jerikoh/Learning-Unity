using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testColliderMelee : MonoBehaviour
{
    void Start()
    {
        InputManager.EventPlayerTrigger += Test;
        InputManager.EventPlayerReload += Test;
    }

    void OnDisable()
    {
        InputManager.EventPlayerTrigger -= Test;
        InputManager.EventPlayerReload -= Test;
    }

    void Test(){Debug.Log("TEST OK, ESCUCHO EL EVENTO Y REACCIONÓ");}
}
