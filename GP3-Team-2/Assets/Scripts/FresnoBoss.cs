using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FresnoBoss : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    GameObject mainCharacter;

    [Header("Health Parameters")]
    public float enemyHealth;
    public float maxEnemyHealth;
    bool isCapturable;

    Animator animator;


    [Header("Attack Parameters")]
    public float damage = -10f;
    public float attackRange = 1f;
    bool alreadyAttacked = false;
    public float timeBetweenAttacks;

    [Header("Healthbar")]
    public Image health;

    public float chaseRange = 50f;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged Variant").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = (float)enemyHealth / maxEnemyHealth;

        if (ChaseRangeCheck())
        {
            ChasePlayer();
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(isCapturable) 
        {
            animator.SetBool("isStunned", true);
            animator.SetBool("isMoving", false);
            agent.SetDestination(transform.position);
        }
        if(AttackRangeCheck())
        {
            Attack();
        }
        CheckCapturable();
        CheckHealth();

        if(enemyHealth <= 2499)
        {
            chaseRange = 500f;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttacks * Time.deltaTime);
        }
    }

    private void CheckCapturable()
    {
        if (enemyHealth <= 100)
        {
            isCapturable = true;
        }
    }

    private void CheckHealth()
    {


        if (enemyHealth > maxEnemyHealth)
        {
            enemyHealth = maxEnemyHealth;
        }
        else if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }
    }

    bool AttackRangeCheck()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {   
        if (other.gameObject.tag == "Bullet")
        {
            enemyHealth -= 50f;
        }

        if (other.gameObject.tag == "Player" && !alreadyAttacked)
        {
            other.gameObject.GetComponent<StatsInventoryManager>().UpdateHealth(damage);


            Debug.Log("Enemy Attacked");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        if (isCapturable)
        {
            if(other.gameObject.tag == "Net")
            {
                Destroy(gameObject);
                LevelStatTracker.instance.Boss();
            }
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        Debug.Log("Enemy Attack CD");
    }

    bool ChaseRangeCheck()
    {
        float distanceToPlayer2 = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer2 <= chaseRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
