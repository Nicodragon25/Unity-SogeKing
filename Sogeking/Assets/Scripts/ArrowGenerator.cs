using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;

    GameObject arrow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void arrowSpawn(Vector3 Rotation, float arrowspeed)
    {
        arrow = Instantiate(arrowPrefab, transform.position, transform.rotation = Quaternion.Euler(Rotation));
        arrow.GetComponent<ArrowController>().speed = arrowspeed;
    }

    public void arrowIndependence()
    {
        arrow.transform.parent = null;
    }
}
