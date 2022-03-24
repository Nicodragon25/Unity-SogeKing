using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    [SerializeField] GameObject parent;


    private void Awake()
    {
        parent = transform.root.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (parent.name)
        {
            case "Enemy(Clone)":
                parent.GetComponent<MediumEnemy>().OnChildrenCollision(this.gameObject.name, collision);
                break;
            case "Heavy Enemy(Clone)":
                parent.GetComponent<HeavyEnemy>().OnChildrenCollision(this.gameObject.name, collision);
                break;
            case "Light Enemy(Clone)":
                parent.GetComponent<LightEnemy>().OnChildrenCollision(this.gameObject.name, collision);
                break;
        }

    }
}
