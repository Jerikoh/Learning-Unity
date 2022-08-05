using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnDelay = 1f;
    public float spawnInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, 3);
        Instantiate(enemyPrefab[enemyIndex], transform);
    }
}
