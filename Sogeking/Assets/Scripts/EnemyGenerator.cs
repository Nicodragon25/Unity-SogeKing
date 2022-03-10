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

    public int maxStrongEnemies;
    public int maxNormalEnemies;
    bool strongEnemyEliminated = false;
    bool normalEnemyEliminated = false;
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

        if (enemies[randomEnemy].gameObject.name == "Strong Enemy")
        {
            maxStrongEnemies--;
        }
        if (maxStrongEnemies <= 0 && !strongEnemyEliminated)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject strongEnemy = enemies[i];
                if (strongEnemy.name.Contains("Strong"))
                {
                    enemies.Remove(strongEnemy);
                    strongEnemyEliminated = true;
                }
            }
        }
        if (enemies[randomEnemy].gameObject.name == "Enemy")
        {
            maxNormalEnemies--;
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
    }
}
