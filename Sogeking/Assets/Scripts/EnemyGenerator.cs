using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();


    GameObject enemy;
    float minXDistance = -50;
    float maxXDistance = -30;
    float minZDistance = -14;
    float maxZDistance = 14;

    float TimePass;
    public float SpawnCD;

    int spawnedEnemies;
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
                if (GameObject.FindGameObjectWithTag("Door")??false)
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

        if(spawnedEnemies < 2 && TimePass > SpawnCD)
        {
            enemy = Instantiate(enemies[0], spawnPosition, Quaternion.identity);
            spawnedEnemies++;
            TimePass = 0;
        }
        if (spawnedEnemies >= 2 && TimePass > SpawnCD)
        {
            enemy = Instantiate(enemies[randomEnemy], spawnPosition, Quaternion.identity);
            spawnedEnemies++;
            TimePass = 0;
        }
        switch (enemy.name)
        {
            case "Enemy(Clone)":
                maxNormalEnemies--;
                break;
            case "Heavy Enemy(Clone)":
                maxStrongEnemies--;
                break;
            case "Light Enemy(Clone)":
                maxFastEnemies--;
                break;
        }

        if (maxStrongEnemies <= 0 && !strongEnemyEliminated)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject slowEnemy = enemies[i];
                if (slowEnemy.name == "Heavy Enemy")
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
                if (fastEnemy.name == "Light Enemy")
                {
                    enemies.Remove(fastEnemy);
                    fastEnemyEliminated = true;
                }
            }
        }

    }
}
