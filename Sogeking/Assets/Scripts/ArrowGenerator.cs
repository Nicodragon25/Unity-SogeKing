using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject player;

    bool canShoot = true;
    public float shootingCoolDown;
    float timePass;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            Shoot(player.transform.rotation.eulerAngles);
            canShoot = false;
        }
        if (!canShoot)
        {
            timePass += Time.deltaTime;
        }
        if (timePass >= shootingCoolDown)
        {
            canShoot = true;
            timePass = 0;
        }
    }

    private void Shoot(Vector3 Rotation)
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation = Quaternion.Euler(Rotation));
    }
}
