using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeysCount : MonoBehaviour
{
    [SerializeField] Text keysText;

    void FixedUpdate()
    {
        keysText.text = "Keys: " + GameManager.Keys.ToString();
    }
}
