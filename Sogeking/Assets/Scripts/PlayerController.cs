using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject arrowGenerator;
    public GameObject arrow;
    public GameObject playerViewPoint;
    public GameObject bow;
    public GameObject camera;
    Rigidbody rb;

    public float speed;
    public float startSpeed;

    public bool loadedArrow = false;

    //public float lmbDownTime;
    public float arrowSpeed;
    public float finalArrowSpeed;


    public float minArrowSpeed;
    public float maxArrowSpeed;
    //public float shotTimePass;
    float timePassSpawn;
    float timePassShoot;
    public float arrowSCD;
    bool canArrowSpawn;
    public bool canArrowShoot;

    void Start()
    {
        camera = Camera.main.gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        startSpeed = speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            arrowGenerator.GetComponent<ArrowGenerator>().arrowSpawn(playerViewPoint.transform.rotation.eulerAngles);
            arrow = arrowGenerator.GetComponent<ArrowGenerator>().arrow;
            loadedArrow = true;
            canArrowSpawn = false;
            finalArrowSpeed = minArrowSpeed;
            GameManager.Instance.powerBar.gameObject.SetActive(true);
            GameManager.Instance.powerBar.value = minArrowSpeed;
        }
        if (Input.GetMouseButtonUp(1))
        {
            bow.GetComponent<Animator>().SetBool("IsAiming", false);
            camera.GetComponent<Animator>().SetBool("IsAiming", false);
            if (loadedArrow)
            {
                arrowGenerator.GetComponent<ArrowGenerator>().arrow.GetComponent<ArrowController>().loadedArrowDestroy();
                GameManager.Instance.powerBar.gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButton(1))
        {
            bow.GetComponent<Animator>().SetBool("IsAiming", true);
            camera.GetComponent<Animator>().SetBool("IsAiming", true);
            if (!loadedArrow && canArrowSpawn)
            {
                arrowGenerator.GetComponent<ArrowGenerator>().arrowSpawn(playerViewPoint.transform.rotation.eulerAngles);
                arrow = arrowGenerator.GetComponent<ArrowGenerator>().arrow;
                loadedArrow = true;
                canArrowSpawn = false;
                finalArrowSpeed = minArrowSpeed;
                GameManager.Instance.powerBar.gameObject.SetActive(true);
                GameManager.Instance.powerBar.value = minArrowSpeed;
                bow.GetComponent<Animator>().SetBool("HasShot", false);
            }

            if (!canArrowSpawn && loadedArrow == false)
            {
                timePassSpawn += Time.deltaTime;
            }
            if (timePassSpawn >= arrowSCD)
            {
                canArrowSpawn = true;
                timePassSpawn = 0;
            }
            if (Input.GetMouseButton(0))
            {
                if (finalArrowSpeed < maxArrowSpeed) finalArrowSpeed += Time.deltaTime * arrowSpeed;
                if (finalArrowSpeed >= maxArrowSpeed)
                {
                    finalArrowSpeed = maxArrowSpeed;
                }
                GameManager.Instance.ChargePower(finalArrowSpeed);

                bow.GetComponent<Animator>().Play("Draw");
                arrow.GetComponent<Animator>().Play("ArrowDraw");
            }
            if (Input.GetMouseButtonUp(0) && loadedArrow)
            {
                //shotTimePass = Time.time - lmbDownTime;
                //if (shotTimePass <= 2) finalArrowSpeed = arrowSpeed * shotTimePass;
                //if (shotTimePass > 2) finalArrowSpeed = arrowSpeed * 2;

                arrow.GetComponent<Animator>().enabled = false;
                Shoot(finalArrowSpeed);
                loadedArrow = false;
                bow.GetComponent<Animator>().SetBool("HasShot", true);
                //canArrowShoot = false;
                //lmbDownTime = 0;

            }
            if (!canArrowShoot) timePassShoot += Time.deltaTime;
            if (timePassShoot >= arrowSCD)
            {
                canArrowShoot = true;
                timePassShoot = 0;
            }
        }

    }
    private void FixedUpdate()
    {
        if (Input.GetKey("w")) Move(Vector3.forward);
        if (Input.GetKey("s")) Move(Vector3.back);
        if (Input.GetKey("a")) Move(Vector3.left);
        if (Input.GetKey("d")) Move(Vector3.right);
    }
    void Move(Vector3 direction)
    {
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void Shoot(float ShotSpeed)
    {
        arrowGenerator.GetComponent<ArrowGenerator>().arrowShot(ShotSpeed, Mathf.Ceil(finalArrowSpeed / 4));
        GameManager.Instance.powerBar.gameObject.SetActive(false);
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
