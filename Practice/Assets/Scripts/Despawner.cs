using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    [SerializeField] float entityLifetime = 5;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Despawn", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Despawn()
    {
        Destroy(gameObject, entityLifetime);
    }
}
