using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float startSpeed;
    public GameObject arrowGenerator;
    public GameObject arrow;

    public bool loadedArrow = false;
    public bool shootedArrow = false;

    public float lmbDownTime;
    public float arrowSpeed;
    public float finalArrowSpeed;
    public float timePass;
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

        if (Input.GetMouseButtonDown(1))
        {
            if (loadedArrow == false)
            {
                arrowGenerator.GetComponent<ArrowGenerator>().arrowSpawn(arrowGenerator.GetComponent<ArrowGenerator>().player.transform.rotation.eulerAngles);
                loadedArrow = true;
                shootedArrow = false;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (shootedArrow == false)
            {
                GameObject nonShotArrow = GameObject.FindGameObjectWithTag("LoadedArrow");
                nonShotArrow.GetComponent<ArrowController>().loadedArrowDestroy();
                loadedArrow = false;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (loadedArrow)
            {
                lmbDownTime = Time.time;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (loadedArrow)
            {
                timePass = Time.time - lmbDownTime;
                finalArrowSpeed = arrowSpeed * timePass;
                arrowGenerator.GetComponent<ArrowGenerator>().arrowIndependence();
                Shoot(finalArrowSpeed);
                shootedArrow = true;
                loadedArrow = false;
                lmbDownTime = 0;

            }
        }
    }

    void Move(Vector3 direction)
    {
        transform.Translate( direction * speed * Time.deltaTime);

    }

    void Shoot(float ShotSpeed)
    {
        arrow = GameObject.FindGameObjectWithTag("LoadedArrow");
        arrow.GetComponent<ArrowController>().ArrowMovement(ShotSpeed);
        arrow.tag = "ShotArrow";
    }
}
