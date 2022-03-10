using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public float Hp;
    public Animator animator;
    public GameObject damageText;
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
        if (Hp > 0) animator.Play("pushed");
        if (Hp <= 0)
        {
            animator.Play("died");
            GetComponentInChildren<MeshCollider>().enabled = false;
        }

        Vector3 spawnPos = new Vector3(0f, 1f, 0f);
        DamageIndicator dummyIndicator = Instantiate(damageText, spawnPos, Quaternion.identity).GetComponent<DamageIndicator>();
        dummyIndicator.SetDamageNumber(dmg);
        dummyIndicator.transform.SetParent(gameObject.transform, false);
    }



    public void OnChildrenCollision(string name, Collision other)
    {
        switch (name)
        {
            case "Body":
                if (other.gameObject.layer == 31)
                {
                    TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg);
                }
                break;
            case "Head":
                if (other.gameObject.layer == 31)
                {
                    TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowHsDmg);
                }
                break;
        }
    }
}
