using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    public float arrowSpeed;
    public float destroyTime;
    public Rigidbody rb;
    public bool canMove = false;
    public Quaternion crashRotation;
    public float torque;
    Animator arrowAnim;
    bool isStopped;

    public float arrowDmg;
    public float arrowHsDmg;

    public bool hasTrail;
    void Start()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        arrowAnim = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
    }
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
            arrowHsDmg = arrowDmg * 1.5f;
            // transform.rotation = Quaternion.LookRotation();
        }
        if(!isStopped) transform.LookAt(transform.position + rb.velocity);
        if (isStopped) 
        { 
            transform.rotation = crashRotation; 
            rb.Sleep();
        }
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
        //Debug.Log(other.gameObject.name);
        switch (this.gameObject.tag)
        {
            case "Normal":
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                crashRotation = transform.rotation;
                isStopped = true;
                gameObject.GetComponent<Collider>().isTrigger = true;
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
    }

    public void AnimatorSwitch()
    {
        if (arrowAnim.enabled == false)
        {
            arrowAnim.enabled = true;
        }
        if (arrowAnim.enabled == true)
        {
            arrowAnim.enabled = false;
        }
    }
}
