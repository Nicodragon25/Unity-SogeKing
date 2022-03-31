using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy : EnemyController
{
    public int attacksRemaining;
    bool lightCanAttack = true;
    protected override void Attack()
    {
        if(lightCanAttack) base.Attack();
        if (attacksRemaining > 0)
        {
            attacksRemaining--;
        }
        if (attacksRemaining <= 0)
        {
            lightCanAttack = false;
            //gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            //Destroy(gameObject, 3f);
            Die();
        }
    }
}
