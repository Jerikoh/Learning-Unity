using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergyMetter : MonoBehaviour
{
    [SerializeField] Text energyText;

    void Update()
    {
        energyText.text = "Energy: " + GameManager.Energy.ToString("0.00");
    }
}
