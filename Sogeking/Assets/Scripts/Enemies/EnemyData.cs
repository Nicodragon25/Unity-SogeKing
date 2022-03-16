using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public int enemyDamage;
    public float enemyHp;

    public float moveCooldown;
    public float attackCooldown;


    [Header("Ray")]
    [Range(0.0f, 5f)]
    public float rayDistance;
    [Range(0.0f, 30f)]
    public float rayDistanceDoor;
}
