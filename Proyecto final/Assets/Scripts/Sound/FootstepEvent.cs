using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FootstepEvent : MonoBehaviour
{
    public static event Action EventPlayerStep;
    bool canStep = true;
    bool isMoving;
    float speedPerSec;
    Vector3 oldPosition;

    void FixedUpdate()
    {
        speedPerSec = Vector3.Distance(oldPosition, transform.position) / Time.deltaTime;
        oldPosition = transform.position;

        if (speedPerSec > 0.95f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void DoStep()
    {
        if (canStep && isMoving)
        {
            EventPlayerStep?.Invoke();
            canStep = false;
            Invoke("Activator", 0.25f);
        }
    }

    void Activator()
    {
        canStep = true;
    }
}
