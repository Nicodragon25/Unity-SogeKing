using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject parent = transform.parent.gameObject;
        parent.GetComponent<DummyController>().OnChildrenCollision(gameObject.name, collision);
    }
}
