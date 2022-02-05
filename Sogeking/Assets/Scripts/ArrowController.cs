using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>().loadedArrow == true)
        {
            gameObject.tag = "LoadedArrow";
        }
        if (gameObject.tag == "ShotArrow")
        {
            GameObject player = GameObject.Find("Player");
            float shotSpeed = player.GetComponent<PlayerController>().finalArrowSpeed;
            ArrowMovement(shotSpeed);
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
}
