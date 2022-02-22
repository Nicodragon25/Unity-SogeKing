using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    public float ArrowDmg;
    public float destroyTime;
    public Rigidbody rb;
    public bool canMove = false;
    public Quaternion crashRotation;
    public float torque;
    public float gravity = 15;
    bool isStopped;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
        {
            ArrowMovement(speed);
            canMove = false;
            isStopped = false;
        }
        if (rb.velocity != Vector3.zero)
        {
            Destroy(gameObject, destroyTime);
            // transform.rotation = Quaternion.LookRotation();
        }
        if(!isStopped) transform.LookAt(transform.position + rb.velocity);
        if (isStopped) 
        { 
            transform.rotation = crashRotation; 
            rb.Sleep();
        }
        //if (rb.velocity == Vector3.zero && !alreadyshot) hasForce = false;
        /*if (transform.rotation.x <= initialRotation.x)
        {
            //rb.angularVelocity += transform.TransformDirection(Vector3.right * rb.velocity.z * Time.deltaTime);
            rb.AddRelativeTorque(Vector3.left * torque * Time.deltaTime);
            Debug.Log("rotar");
        }*/
    }
    public void ArrowMovement(float arrowSpeed)
    {
        //transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
        rb.AddRelativeForce(Vector3.forward * arrowSpeed, ForceMode.Impulse);
    }
    public void loadedArrowDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        switch (this.gameObject.tag)
        {
            case "Normal":
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                crashRotation = transform.rotation;
                isStopped = true;

                break;
            case "Fire":
                //speed = 0;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                crashRotation = transform.rotation;
                isStopped = true;
                if (other.gameObject.CompareTag("WoodenWall"))
                {
                    Destroy(other.gameObject, 1f);
                }
                break;
                
        }
        if (other.transform.CompareTag("Enemy"))
        {
            GameObject enemyCollider = other.gameObject;
            enemyCollider.GetComponentInParent<DummyController>().TakeDamage(ArrowDmg);
        }
    }
}
