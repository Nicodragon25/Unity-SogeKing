using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ArrowMovement(speed);
        if (speed != 0)
        {
            Destroy(gameObject, destroyTime);
        }
    }

    public void ArrowMovement(float arrowSpeed)
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
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
                speed = 0;
                break;
            case "Fire":
                speed = 0;
                if (other.gameObject.CompareTag("WoodenWall"))
                {
                    Destroy(other.gameObject, 1f);
                }
                break;
                
        }
        
    }
}
