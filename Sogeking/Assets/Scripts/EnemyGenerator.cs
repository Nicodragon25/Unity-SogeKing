using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();


    GameObject enemy;
    GameManager gameManager;
    float minXDistance = -50;
    float maxXDistance = -30;
    float minZDistance = -14;
    float maxZDistance = 14;

    float TimePass;
    public float SpawnCD;

    int rareEnemy = 0;
    public int normalEnemy = 0;
    public int generatedNumber;
    int spawnedEnemies;
    public int maxNormalEnemies;
    public int maxStrongEnemies;
    public int maxFastEnemies;
    bool strongEnemyEliminated = false;
    bool normalEnemyEliminated = false;
    bool fastEnemyEliminated = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        int totalEnemies = maxFastEnemies + maxNormalEnemies + maxStrongEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimePass > SpawnCD)
        {
            if (enemies.Count > 0)
            {
                if (GameObject.FindGameObjectWithTag("Door") ?? false)
                {
                    SpawnEnemy();
                    TimePass = 0;
                }
            }
        }
        else TimePass += Time.deltaTime;
    }


    void SpawnEnemy()
    {
        float RandomPositionX = Random.Range(minXDistance, maxXDistance);
        float RandomPositionZ = Random.Range(minZDistance, maxZDistance);
        int randomEnemy = Random.Range(0, enemies.Count);
        Vector3 spawnPosition = new Vector3(RandomPositionX, 0.5f, RandomPositionZ);
       
        generatedNumber = Random.Range(0, 100);
        if (generatedNumber >= 0 && generatedNumber < 50 && normalEnemy <= 4)
        {
            enemy = Instantiate(enemies[0], spawnPosition, Quaternion.identity);
            rareEnemy = 0;
            normalEnemy++;
            TimePass = 0;
        }
        if (generatedNumber >= 50 && generatedNumber < 75 && rareEnemy == 0)
        {
            enemy = Instantiate(enemies[1], spawnPosition, Quaternion.identity);
            rareEnemy = 1;
            normalEnemy = 0;
            TimePass = 0;
        }
        if (generatedNumber >= 75 && generatedNumber < 100 && rareEnemy == 0)
        {
            enemy = Instantiate(enemies[2], spawnPosition, Quaternion.identity);
            rareEnemy = 1;
            normalEnemy = 0;
            TimePass = 0;
        }
        if (generatedNumber >= 0 && generatedNumber < 50 && normalEnemy > 4)
        {
            generatedNumber = Random.Range(50, 100);
            if (generatedNumber >= 50 && generatedNumber < 75 && rareEnemy == 0)
            {
                enemy = Instantiate(enemies[1], spawnPosition, Quaternion.identity);
                rareEnemy = 1;
                TimePass = 0;
            }
            if (generatedNumber >= 75 && generatedNumber < 100 && rareEnemy == 0)
            {
                enemy = Instantiate(enemies[2], spawnPosition, Quaternion.identity);
                rareEnemy = 1;
                TimePass = 0;
            }
            normalEnemy = 0;
        }
    }
}
