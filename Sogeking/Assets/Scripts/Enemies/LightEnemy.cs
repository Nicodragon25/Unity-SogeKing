using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : EnemyController
{
    public int attacksRemaining;
    protected override void Attack()
    {
        base.Attack();
        if (attacksRemaining > 0)
        {
            attacksRemaining--;
        }
        if (attacksRemaining <= 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
