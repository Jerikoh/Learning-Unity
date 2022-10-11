using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPdeath : MonoBehaviour
{
    // [] localizar y quitar todos los componentes de este script, y hacer una lista de GO que desactivar todos juntos, para tenerlo mas manejable
    void Start()
    {
        PlayerCollision.EventPlayerDeath += DeleteSpace;
    }

    
    void DeleteSpace()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        PlayerCollision.EventPlayerDeath -= DeleteSpace;
    }
}
