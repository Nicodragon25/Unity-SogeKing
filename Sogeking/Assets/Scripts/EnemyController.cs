using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject door;
    public Rigidbody rb;
    public float speed;
    public float damage;
    Vector3 lookPos;
    float maxSpeed;

    void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        lookPos = door.transform.localPosition - transform.position;
        lookPos.y = 0;
        maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
    }
    private void FixedUpdate()
    {


        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            rb.velocity = Vector3.zero;
            speed = 0f;
        }
        other.gameObject.GetComponent<DoorController>().TakeDamage(damage);
    }





}
