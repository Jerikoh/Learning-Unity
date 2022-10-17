using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPRiseAndFall : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [SerializeField][Range(0f, 10f)] float riseSpeed = 1f;
    [SerializeField][Range(0f, 10f)] float fallSpeed = 1f;
    [SerializeField][Range(0f, 10f)] float stayTime = 0f;
    bool rise = false;
    bool fall = false;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.weight = 0;
        //EVENTO
        PlayerCollision.EventPlayerDamage += RiseStart;
    }

    void Update()
    {
        if (rise)
        {
            if (volume.weight < 1)
            {
                //aumentando volume.weight
                volume.weight += Time.deltaTime * riseSpeed;
            }
            else //if (volume.weight >= 1)
            {
                Invoke("SwitchToFall", stayTime);
            }
        }
        if (fall)
        {
            if (volume.weight > 0)
            {
                //reduciendo volume.weight
                volume.weight -= Time.deltaTime * fallSpeed;
            }
            else if (volume.weight <= 0)
            {
                fall = false;
            }
        }
    }

    public void RiseStart()
    {
        rise = true;
    }

    void SwitchToFall()
    {
        rise = false;
        fall = true;
    }

    void OnDisable()
    {
        PlayerCollision.EventPlayerDeath -= RiseStart;
    }
}