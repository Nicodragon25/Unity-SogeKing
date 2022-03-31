using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : EnemyController
{

    bool firstMove = false;
    float firstMoveCd = 0.5f;
    float EnemyTimePass;

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
            canTryMoving = false;
            canMove = false;
            EnemyTimePass += Time.deltaTime;
        }
        if (EnemyTimePass > firstMoveCd) firstMove = true;
    }
}
