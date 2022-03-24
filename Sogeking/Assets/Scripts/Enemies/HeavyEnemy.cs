using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : EnemyController
{
    bool firstMove = false;

    float firstMoveCd = 3.267f;
    float heavyEnemyTimePass;
    protected override void Start()
    {
        base.Start();
        canMove = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!firstMove) 
        {
            canMove = false;
            heavyEnemyTimePass += Time.deltaTime;
        }
        if (heavyEnemyTimePass > firstMoveCd) firstMove = true;

        if (canMove) gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
        if (!canMove) gameObject.GetComponent<Animator>().SetBool("IsMoving", false);
    }
}
