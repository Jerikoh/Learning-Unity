using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingLight : MonoBehaviour
{
    private Light myLight;
    [SerializeField] float maxIntensity = 5f;
    [SerializeField] float minIntensity = 1f;
    [SerializeField] float pulseSpeed = 1f;
    private float targetIntensity = 1f;
    private float currentIntensity;


    void Start()
    {
        myLight = GetComponent<Light>();
    }
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        myLight.intensity = currentIntensity;
    }
}