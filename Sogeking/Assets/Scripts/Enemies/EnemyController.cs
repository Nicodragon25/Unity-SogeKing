using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] protected EnemyData enemyStats;
    GameManager gameManager;
    public GameObject door;
    public GameObject shootPoint;
    public GameObject damageText;
    Vector3 lookPos;
    public LayerMask fortLayer;
    public LayerMask moveLayer;
    RaycastHit hit;
    RaycastHit hitDoor;
    Vector3 offset = new Vector3(0, 0.1f, 0);


    bool canAttack = true;
    float timePass;
    protected bool canMove = true;
    protected bool canTryMoving = true;
    float moveTimePass;

    bool isDead = false;
    float hitTimePass;
    float getHitCoolDown = 0.3f;
    protected float dieTimer = 5;

    [SerializeField] float RuntimeEnemyHp;
    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        //shootPoint = gameObject.transform.GetChild(0).gameObject;
        shootPoint = gameObject.transform.Find("ShootPoint").gameObject;
        door = GameObject.FindGameObjectWithTag("Door");
        fortLayer = LayerMask.GetMask("Fort");
        lookPos = door.transform.localPosition - transform.position;
        lookPos.y = 0;
        RuntimeEnemyHp = enemyStats.enemyHp;
    }
    protected virtual void Update()
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


        if (canMove) gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
        if (!canMove) gameObject.GetComponent<Animator>().SetBool("IsMoving", false);


        hitTimePass += Time.deltaTime;
    }
    void EnemyMovement()
    {
        if (canMove && !isDead)
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
        if (canAttack && !isDead)
        {
            if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hitDoor, enemyStats.rayDistanceDoor, fortLayer))
            {
                if (hitDoor.collider.CompareTag("Door") && !canMove && !isDead)
                {
                    Attack();
                    canAttack = false;
                    timePass = 0;
                }
            }
        }
        if (Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, enemyStats.rayDistance, moveLayer))
        {
            canMove = false;
            moveTimePass = 0;
            canTryMoving = false;
        }
        if (!Physics.Raycast(shootPoint.transform.position, shootPoint.transform.TransformDirection(Vector3.forward), out hit, enemyStats.rayDistance, moveLayer) && !isDead)
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
        gameObject.GetComponent<Animator>().Play("Attack");
    }
    void TakeDamage(float dmg)
    {
        DamageIndicator enemyIndicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        if (!isDead)
        {
            enemyIndicator.SetDamageNumber(dmg);
            gameObject.GetComponent<Animator>().Play("GetHit");
        }
        
        //enemyIndicator.transform.SetParent(gameObject.transform, true);
        RuntimeEnemyHp -= dmg;
        if (RuntimeEnemyHp <= 0)
        {
            enemyIndicator.gameObject.transform.parent = null;
            Die();
        }
        //enemyIndicator.GetComponent<DamageIndicator>().player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnChildrenCollision(string name, Collision other)
    {
        if(other.gameObject.layer == 31)
        {
            if(hitTimePass > getHitCoolDown && !isDead)
            {
                gameManager.HitMarker();
                if (name == "head")
                {
                    if (other.gameObject.CompareTag("Fire"))
                    {
                        TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg * 2f);
                    }
                    else
                    {
                        TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg * 1.25f);
                    }
                }
                if (name != "head")
                {
                    if (other.gameObject.CompareTag("Fire"))
                    {
                        TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg * 1.5f);
                    }
                    else
                    {
                        TakeDamage(other.gameObject.GetComponent<ArrowController>().arrowDmg);
                    }
                }
                hitTimePass = 0;
            }
        }
        
    }

    protected virtual void Die()
    {
        gameObject.GetComponent<Animator>().Play("Die");
        GameManager.Instance.scoreController.AddScore(enemyStats.enemyHp);
        Destroy(gameObject, dieTimer);
        gameObject.layer = 25;
        isDead = true;

        canMove = false;
        canAttack = false;
    }
}
