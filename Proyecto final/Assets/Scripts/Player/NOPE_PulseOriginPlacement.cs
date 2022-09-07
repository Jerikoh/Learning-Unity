using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOPE_PulseOriginPlacement : MonoBehaviour
{
    /* [] IMPORTANTE: no se puede coordinar el teleport con el fin del pulso, por lo tanto se entrecorta.
    Dudo que pueda conseguir lo opuesto dada la estructura del shader-script y mi falta de conocimiento,
    por lo que tendré que conformarme con que el pulso siga al player constantemente.
    Otra para "safar" sería que el pulso persiga al player con cierto retardo, podría servir?    
    */
    [SerializeField] GameObject playerPosition;
    [SerializeField][Range(0f, 5f)] float teleportTimeInterval = 1f;
    bool canTeleport = true;

    void Update()
    {
        if (canTeleport) //intente hacer un invokerepeating en start a teleportpulse pero no funciono, no se por que
        {
            canTeleport = false; //no se si es vano al estar en el update, pero lo pongo para que no invoke cientos de temporizadores por segundo (espero que lo esté evitando así)
            Invoke("TeleportPulse", teleportTimeInterval);
            Invoke("resetCanTeleport", teleportTimeInterval);
        }
    }

    void TeleportPulse()
    {
        transform.position = playerPosition.transform.position;
    }

    void resetCanTeleport()
    {
        canTeleport = true;
    }
}
