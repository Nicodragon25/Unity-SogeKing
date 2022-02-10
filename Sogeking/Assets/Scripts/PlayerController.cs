using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float startSpeed;
    public GameObject arrowGenerator;
    public GameObject arrow;
    public GameObject playerViewPoint;

    public bool loadedArrow = false;

    public float lmbDownTime;
    public float arrowSpeed;
    public float finalArrowSpeed;
    public float shotTimePass;

    float timePass;
    public float arrowSCD;
    bool canArrowSpawn;


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
            arrowGenerator.GetComponent<ArrowGenerator>().arrowSpawn(playerViewPoint.transform.rotation.eulerAngles);
            loadedArrow = true;
            canArrowSpawn = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (loadedArrow)
            {
                arrowGenerator.GetComponent<ArrowGenerator>().arrow.GetComponent<ArrowController>().loadedArrowDestroy();
            }
        }

        if (Input.GetMouseButton(1))
        {
            if (!loadedArrow && canArrowSpawn)
            {
                arrowGenerator.GetComponent<ArrowGenerator>().arrowSpawn(playerViewPoint.transform.rotation.eulerAngles);
                loadedArrow = true;
                canArrowSpawn = false;
            }

            if (!canArrowSpawn && loadedArrow == false)
            {
                timePass += Time.deltaTime;
            }
            if (timePass >= arrowSCD)
            {
                canArrowSpawn = true;
                timePass = 0;
            }
            if (Input.GetMouseButtonDown(0))
            {
                lmbDownTime = Time.time;
            }
            if (Input.GetMouseButtonUp(0))
            {
                shotTimePass = Time.time - lmbDownTime;
                if (shotTimePass <= 2) finalArrowSpeed = arrowSpeed * shotTimePass;
                if (shotTimePass > 2) finalArrowSpeed = arrowSpeed * 2;
                Shoot(finalArrowSpeed);
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
        arrowGenerator.GetComponent<ArrowGenerator>().arrowShot(ShotSpeed);
    }
    void ArrowSwitch(string arrowType)
    {
        switch (arrowType)
        {
            case "NormalArrow":
                arrowGenerator.GetComponent<ArrowGenerator>().arrowTypes = ArrowGenerator.ArrowTypes.Normal;
                break;
            case "FireArrow":
                arrowGenerator.GetComponent<ArrowGenerator>().arrowTypes = ArrowGenerator.ArrowTypes.Fire;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Normal":
                ArrowSwitch("NormalArrow");
                break;
            case "Fire":
                ArrowSwitch("FireArrow");
            break;
        }
    }
}
