using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthMetter : MonoBehaviour
{
    [SerializeField] Text healthText;

    void FixedUpdate()
    {
        healthText.text = "Health: " + GameManager.Health.ToString();
    }
}
