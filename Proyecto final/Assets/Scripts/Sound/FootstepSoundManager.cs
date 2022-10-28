using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] stepSounds;
    int randomIndex;

    void Start()
    {
        FootstepEvent.EventPlayerStep += PlayStep;
    }

    void PlayStep()
    {
        randomIndex = UnityEngine.Random.Range(0, 7);
        stepSounds[randomIndex].Play();
    }

    void OnDisable()
    {
        FootstepEvent.EventPlayerStep -= PlayStep;
    }
}
