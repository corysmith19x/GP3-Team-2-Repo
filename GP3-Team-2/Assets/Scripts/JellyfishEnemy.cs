using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyfishEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;

    [Header("Health Parameters")]
    public float enemyHealth;
    public float maxEnemyHealth = 200f;

    [Header("Attack Parameters")]
    public float attackRange = 10f;
    bool alreadyAttacked = false;
    public float timeBetweenAttacks;
    public GameObject projectile;

    [Header("Healthbar")]
    public Image health;

    public GameObject[] itemDrops;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged Variant").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer(); 
        CheckHealth();

        health.fillAmount = (float)enemyHealth / maxEnemyHealth;

        if(AttackRangeCheck())
        {
            Attack();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void CheckHealth()
    {
        if(enemyHealth > maxEnemyHealth)
        {
            enemyHealth = maxEnemyHealth;
        }
        else if (enemyHealth < 0)
        {
            enemyHealth = 0;
        }

        if (enemyHealth == 0)
        {
            ItemDrop();
            Destroy(gameObject);
            LevelStatTracker.instance.Grunts();
        }
    }

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            // Make the enemy sit still
            agent.SetDestination(transform.position);

            transform.LookAt(player);
            Debug.Log("Enemy Attack being called");
            // Attack code
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            
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

    private void ItemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            Instantiate(itemDrops[i], transform.position, Quaternion.identity);

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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Caterpillar hit");
            enemyHealth -= 50f;
        }
    }
}