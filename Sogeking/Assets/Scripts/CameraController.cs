using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject bow;
    Vector3 target;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(ray.GetPoint(0f), hit.point))
        {
            target = hit.point; // We hit something, aim at that
        }
        else
        {
            target = ray.GetPoint(100); // Distance we're aiming at, could be something else
        }
        bow.transform.LookAt(target);
    }
}
