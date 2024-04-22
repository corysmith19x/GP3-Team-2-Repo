using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SirenEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public Transform firePos;
    Animator anim;

    [Header("Health Parameters")]
    public float enemyHealth;
    public float maxEnemyHealth = 1000f;
    bool isCapturable = false;

    [Header("Attack Parameters")]
    public float attackRange = 30f;
    bool alreadyAttacked = false;
    public float timeBetweenAttacks = 0.5f;
    public GameObject projectile;
    public float forceMultiplier;
    public float upwardForceMultiplier;

    [Header("Healthbar")]
    public Image health;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged Variant").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
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

        CheckHealth();
        if(!isCapturable)
        {
            if (AttackRangeCheck())
            {
                Attack();
            }
        }
        CheckCapturable();
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

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            anim.SetTrigger("Attack");
            // Make the enemy sit still
            agent.SetDestination(transform.position);

            transform.LookAt(player);
            Debug.Log("Enemy Attack being called");
            // Attack code
            Rigidbody rb = Instantiate(projectile, firePos.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forceMultiplier, ForceMode.Impulse);
            rb.AddForce(transform.up * upwardForceMultiplier, ForceMode.Impulse);


            Debug.Log("Enemy Attacked");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Debug.Log("Enemy Attack CD");
    }

    private void CheckCapturable()
    {
        if (enemyHealth <= 100)
        {
            anim.SetBool("isStunned", true);
            Debug.Log("Is capturable");
            isCapturable = true;
        }
    }

    bool AttackRangeCheck()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
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
        
        if (isCapturable)
        {
            if(other.gameObject.tag == "Net")
            {
                Destroy(gameObject);
            }
        }
    }
}
