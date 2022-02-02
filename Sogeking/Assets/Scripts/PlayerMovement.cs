using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float startSpeed;
    // Start is called before the first frame update
    void Start()
    {
       startSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) Move(Vector3.forward);
        if (Input.GetKey("s")) Move(Vector3.back);
        if (Input.GetKey("a")) Move(Vector3.left);
        if (Input.GetKey("d")) Move(Vector3.right);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = startSpeed;
        }

        Input.GetAxis("Mouse X");
        Input.GetAxis("Mouse Y");
        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));
    }

    void Move(Vector3 direction)
    {
        transform.Translate( direction * speed * Time.deltaTime);

    }

}
