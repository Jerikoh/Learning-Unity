using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanControl : MonoBehaviour
{
    Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer> ();
    }

    
    void Update()
    {
        float radius = Mathf.PingPong(Time.time, 5.0f);
        rend.material.SetFloat("_MaskRadius", radius);
    }
}
