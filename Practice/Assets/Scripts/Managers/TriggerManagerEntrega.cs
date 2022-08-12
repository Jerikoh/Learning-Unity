using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagerEntrega : MonoBehaviour
{
    bool transformed = false;

    private void OnTriggerExit(Collider other)
    {
        if (!transformed)
        {
            other.transform.localScale -= transform.localScale * 0.5f;
            transformed = !transformed;
        }
        else if (transformed)
        {
            other.transform.localScale += transform.localScale * 0.5f;
            transformed = !transformed;
        }
        
    }
}
