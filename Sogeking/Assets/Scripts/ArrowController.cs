using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public Rigidbody rb;
    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            ArrowMovement(speed);
            canMove = false;
        }
        if (rb.velocity != Vector3.zero)
        {
            Destroy(gameObject, destroyTime);
            canMove = false;
        }
        //if (rb.velocity == Vector3.zero && !alreadyshot) hasForce = false;
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

        switch (this.gameObject.tag)
        {
            case "Normal":
                //speed = 0;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                break;
            case "Fire":
                //speed = 0;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                if (other.gameObject.CompareTag("WoodenWall"))
                {
                    Destroy(other.gameObject, 1f);
                }
                break;
                
        }
        
    }
}
