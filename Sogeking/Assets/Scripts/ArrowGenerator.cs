using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;

    public GameObject arrow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void arrowSpawn(Vector3 Rotation)
    {
        arrow = Instantiate(arrowPrefab, transform.position, transform.rotation = Quaternion.Euler(Rotation));
        arrow.GetComponent<ArrowController>().speed = 0;
        arrow.transform.parent = transform;
    }

    public void arrowShot(float arrowspeed)
    {
        arrow.transform.parent = null;
        arrow.GetComponent<ArrowController>().speed = arrowspeed;
    }
}
