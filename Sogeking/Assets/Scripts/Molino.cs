using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molino : MonoBehaviour
{
    public GameObject Blade;
    public float bladeSpeed;
    void Start()
    {
        
    }

    private void Update()
    {
        Blade.transform.Rotate(0,0, bladeSpeed * Time.deltaTime);
    }
}
