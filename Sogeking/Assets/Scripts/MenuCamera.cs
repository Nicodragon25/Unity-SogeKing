using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float speed;
    public GameObject rotationPoint;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationPoint.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
