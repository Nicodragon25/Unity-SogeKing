using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject door;
    public GameObject shootPoint;
    public GameObject damageText;
    public Rigidbody rb;
    public float speed;
    public float enemyDamage;
    public float enemyHp;
    Vector3 lookPos;

    public LayerMask fortLayer;
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
            if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hitDoor, rayDistanceDoor, fortLayer))
            {
                if (hitDoor.collider.CompareTag("Door") && !canMove)
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 31)
        {
            TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg);
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
        door.GetComponent<DoorController>().TakeDamage(enemyDamage);
    }
    void TakeDamage(float dmg)
    {
        DamageIndicator enemyIndicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        enemyIndicator.SetDamageNumber(dmg);
        enemyIndicator.transform.SetParent(gameObject.transform, true);
        enemyHp -= dmg;
        if (enemyHp < 0)
        {
            enemyIndicator.gameObject.transform.parent = null;
            Destroy(gameObject);
        }
        //enemyIndicator.GetComponent<DamageIndicator>().player = GameObject.FindGameObjectWithTag("Player");
    }
}
