using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] GameObject refGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = refGameObject.transform.position;
        transform.rotation = refGameObject.transform.rotation; //esto lo puedo evitar de otra forma? con Translate? [] 
    }
}
