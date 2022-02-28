using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject enemyPrefab;

    float minXDistance = -50;
    float maxXDistance = -30;
    float minZDistance = -14;
    float maxZDistance = 14;

    float TimePass;
    public float SpawnCD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimePass > SpawnCD)
        {
            SpawnEnemy();
            TimePass = 0;
        }
        else TimePass += Time.deltaTime;
    }

    void SpawnEnemy()
    {
        float RandomPositionX = Random.Range(minXDistance, maxXDistance);
        float RandomPositionZ = Random.Range(minZDistance, maxZDistance);
        Vector3 spawnPosition = new Vector3(RandomPositionX, 1, RandomPositionZ);
        transform.position = spawnPosition;
        Instantiate(enemyPrefab,spawnPosition, Quaternion.identity);
    }
}
