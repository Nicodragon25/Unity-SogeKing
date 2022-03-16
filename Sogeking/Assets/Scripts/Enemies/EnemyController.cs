using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] protected EnemyData enemyStats;
    public GameObject door;
    public GameObject shootPoint;
    public GameObject damageText;
        Vector3 lookPos;

    public LayerMask fortLayer;
    RaycastHit hit;
    RaycastHit hitDoor;
    Vector3 offset = new Vector3(0, 0.1f, 0);


    bool canAttack = true;
    float timePass;
    bool canMove = true;
    bool canTryMoving = true;
    float moveTimePass;

    float RuntimeEnemyHp;
    private void Start()
    {
        shootPoint = gameObject.transform.GetChild(0).gameObject;
        door = GameObject.FindGameObjectWithTag("Door");
        fortLayer = LayerMask.GetMask("Fort");
        lookPos = door.transform.localPosition - transform.position;
        lookPos.y = 0;
        RuntimeEnemyHp = enemyStats.enemyHp;
    }
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50 * Time.deltaTime);
        EnemyMovement();
        if (!canAttack) timePass += Time.deltaTime;
        if (timePass > enemyStats.attackCooldown) canAttack = true;
        if (canTryMoving)
        {
           moveTimePass += Time.deltaTime;
            canTryMoving = false;
        }
        if (moveTimePass > enemyStats.moveCooldown) canMove = true;
        RayCasting();
    }
    void EnemyMovement()
    {
        if (canMove)
        {
            transform.Translate(Vector3.forward * enemyStats.speed * Time.deltaTime);
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
            if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hitDoor, enemyStats.rayDistanceDoor, fortLayer))
            {
                if (hitDoor.collider.CompareTag("Door") && !canMove)
                {
                    Attack();
                    canAttack = false;
                    timePass = 0;
                }
            }
        }
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, enemyStats.rayDistance))
        {
            canMove = false;
            moveTimePass = 0;
            canTryMoving = false;
        }
        if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, enemyStats.rayDistance))
        {
            canTryMoving = true;
            if (moveTimePass > enemyStats.moveCooldown) canMove = true;
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
        Vector3 enemyGizmosDirection = shootPoint.transform.TransformDirection(Vector3.forward) * enemyStats.rayDistance;
        Gizmos.DrawRay(shootPoint.transform.position, enemyGizmosDirection);

        Gizmos.color = Color.red;
        Vector3 doorGizmosDirection = shootPoint.transform.TransformDirection(Vector3.forward) * enemyStats.rayDistanceDoor;
        Gizmos.DrawRay(shootPoint.transform.position + offset, doorGizmosDirection);
    }
    protected virtual void Attack()
    {

       door.GetComponent<DoorController>().TakeDamage(enemyStats.enemyDamage);
    }
    void TakeDamage(float dmg)
    {
        DamageIndicator enemyIndicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        enemyIndicator.SetDamageNumber(dmg);
        //enemyIndicator.transform.SetParent(gameObject.transform, true);
        RuntimeEnemyHp -= dmg;
        if (RuntimeEnemyHp <= 0)
        {
            enemyIndicator.gameObject.transform.parent = null;
            Destroy(gameObject);
        }
        //enemyIndicator.GetComponent<DamageIndicator>().player = GameObject.FindGameObjectWithTag("Player");
    }
}
