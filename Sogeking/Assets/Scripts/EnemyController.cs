using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject door;
    public GameObject shootPoint;
    public Rigidbody rb;
    public float speed;
    public float damage;
    Vector3 lookPos;


    RaycastHit hit;
    RaycastHit hitDoor;
    Vector3 offset = new Vector3(0, 0.1f, 0);
    [Range(0.0f, 5f)]
    public float rayDistance;
    [Range(0.0f, 30f)]
    public float rayDistanceDoor;

    bool canAttack = true;
    float timePass;
    public float attackCooldown;
    public bool canMove = true;
    public bool canTryMoving = true;
    float moveTimePass;
    public float moveCooldown;

    private void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        lookPos = door.transform.localPosition - transform.position;
        lookPos.y = 0;
        
    }
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
        EnemyMovement();
        if (!canAttack) timePass += Time.deltaTime;
        if (timePass > attackCooldown) canAttack = true;
        if (canTryMoving)
        {
           moveTimePass += Time.deltaTime;
            canTryMoving = false;
        }
        if (moveTimePass > moveCooldown) canMove = true;
    }
    private void LateUpdate()
    {
        RayCasting();
    }
    void EnemyMovement()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (!canMove)
        {
            transform.Translate(Vector3.forward * 0 * Time.deltaTime);
        }

    }
    void RayCasting()
    {
        if (canAttack)
        {
            if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hitDoor, rayDistanceDoor))
            {
                if (hitDoor.collider.CompareTag("Door") && speed == 0)
                {
                    Attack();
                    canAttack = false;
                    timePass = 0;
                }
            }
        }
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            canMove = false;
            moveTimePass = 0;
            canTryMoving = false;
        }
        if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            canTryMoving = true;
            if (moveTimePass > moveCooldown) canMove = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector3 enemyGizmosDirection = shootPoint.transform.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(shootPoint.transform.position, enemyGizmosDirection);

        Gizmos.color = Color.red;
        Vector3 doorGizmosDirection = shootPoint.transform.TransformDirection(Vector3.forward) * rayDistanceDoor;
        Gizmos.DrawRay(shootPoint.transform.position + offset, doorGizmosDirection);
    }
    void Attack()
    {
        door.GetComponent<DoorController>().TakeDamage(damage);
    }
}
