using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public float Hp;
    public Animator animator;

    void Start()
    {
        animator.SetBool("isAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void TakeDamage(float dmg)
    {
        Hp -= dmg;
        if (Hp > 0)animator.Play("pushed");
        if (Hp <= 0)
        {
            animator.Play("died");
            GetComponentInChildren<MeshCollider>().enabled = false;
        }
    }
}
