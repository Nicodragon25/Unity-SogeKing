using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorController : MonoBehaviour
{
    public int doorHP;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int doorDmgTaken)
    {
        doorHP -= doorDmgTaken;
        if (doorHP <= 0)
        {
            gameObject.SetActive(false);
            OnDoorBreak();
        }
    }

    public event Action OnDoorBreak;
}
