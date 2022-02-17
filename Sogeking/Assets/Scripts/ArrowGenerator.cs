using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject[] arrowPrefabs;
    public GameObject player;
    public GameObject arrow;

    public enum ArrowTypes { Normal, Fire, Explosive, Ice}
    public ArrowTypes arrowTypes;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void arrowSpawn(Vector3 Rotation)
    {
        switch (arrowTypes)
        {
            case ArrowTypes.Normal:
                arrow = Instantiate(arrowPrefabs[0], transform.position, transform.rotation = Quaternion.Euler(Rotation));
                arrow.GetComponent<ArrowController>().speed = 0;
                arrow.GetComponent<ArrowController>().rb.isKinematic = true;
                arrow.transform.parent = transform;
            break;
            case ArrowTypes.Fire:
                arrow = Instantiate(arrowPrefabs[1], transform.position, transform.rotation = Quaternion.Euler(Rotation));
                arrow.GetComponent<ArrowController>().speed = 0;
                arrow.GetComponent<ArrowController>().rb.isKinematic = true;
                arrow.transform.parent = transform;
            break;
            case ArrowTypes.Explosive:
                arrow = Instantiate(arrowPrefabs[3], transform.position, transform.rotation = Quaternion.Euler(Rotation));
                arrow.GetComponent<ArrowController>().speed = 0;
                arrow.GetComponent<ArrowController>().rb.isKinematic = true;
                arrow.transform.parent = transform;
                break;

        }
    }

    public void arrowShot(float arrowspeed)
    {
        arrow.transform.parent = null;
        arrow.GetComponent<ArrowController>().speed = arrowspeed;
        arrow.GetComponent<ArrowController>().rb.isKinematic = false;
        arrow.GetComponent<ArrowController>().canMove = true;
    }
}
