using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (doorHP <= 0) Destroy(gameObject, 0.5f);
    }
}
