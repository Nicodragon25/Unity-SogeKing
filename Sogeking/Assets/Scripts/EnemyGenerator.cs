using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    float minXDistance = -50;
    float maxXDistance = -30;
    float minZDistance = -14;
    float maxZDistance = 14;

    float TimePass;
    public float SpawnCD;

    public int maxNormalEnemies;
    public int maxStrongEnemies;
    public int maxFastEnemies;
    bool strongEnemyEliminated = false;
    bool normalEnemyEliminated = false;
    bool fastEnemyEliminated = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TimePass > SpawnCD)
        {
            if (enemies.Count > 0)
            {
                SpawnEnemy();
                TimePass = 0;
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
        
        Instantiate(enemies[randomEnemy], spawnPosition, Quaternion.identity);


        switch (enemies[randomEnemy].GetComponent<EnemyController>().enemyType)
        {
            case EnemyController.EnemyType.normal:
                maxNormalEnemies--;
                break;
            case EnemyController.EnemyType.slow:
                maxStrongEnemies--;
                break;
            case EnemyController.EnemyType.fast:
                maxFastEnemies--;
                break;
        }

        if (maxStrongEnemies <= 0 && !strongEnemyEliminated)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject slowEnemy = enemies[i];
                if (slowEnemy.name == "Slow Enemy")
                {
                    enemies.Remove(slowEnemy);
                    strongEnemyEliminated = true;
                }
            }
        }

        if (maxNormalEnemies <= 0 && !normalEnemyEliminated)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject normalEnemy = enemies[i];
                if (normalEnemy.name == "Enemy")
                {
                    enemies.Remove(normalEnemy);
                    normalEnemyEliminated = true;
                }
            }
        }
        if (maxFastEnemies <= 0 && !fastEnemyEliminated)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject fastEnemy = enemies[i];
                if (fastEnemy.name == "Fast Enemy")
                {
                    enemies.Remove(fastEnemy);
                    fastEnemyEliminated = true;
                }
            }
        }

    }
}
