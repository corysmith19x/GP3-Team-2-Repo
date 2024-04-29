using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaterpillarEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public Transform firePos;

    [Header("Health Parameters")]
    public float enemyHealth;
    public float maxEnemyHealth = 200f;

    [Header("Attack Parameters")]
    public float attackRange = 10f;
    public float chaseRange = 50f;
    bool alreadyAttacked = false;
    public float timeBetweenAttacks;
    public GameObject projectile;

    [Header("Healthbar")]
    public Image health;

    public GameObject[] itemDrops;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged").transform;
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
        if(ChaseRangeCheck())
        {
            ChasePlayer(); 
        }
        CheckHealth();

        health.fillAmount = (float)enemyHealth / maxEnemyHealth;

        if(AttackRangeCheck())
        {
            Attack();
        }

        if(enemyHealth < maxEnemyHealth)
        {
            chaseRange = 200f;
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
            Rigidbody rb = Instantiate(projectile, firePos.position, Quaternion.identity).GetComponent<Rigidbody>();
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
            int r = Random.Range(1, 6);
            Vector3 randomForce = Vector3.zero;
            if (r == 0)
            {
                randomForce = Vector3.forward;
            }
            else if (r == 1)
            {
                randomForce = Vector3.back;
            }
            else if (r == 2)
            {
                randomForce = Vector3.left;
            }
            else if (r == 3)
            {
                randomForce = Vector3.right;
            }
            var instance = Instantiate(itemDrops[i], transform.position, Quaternion.identity);
            instance.GetComponent<Rigidbody>().velocity = (randomForce * 3f) + (Vector3.up * 2f);
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

    bool ChaseRangeCheck()
    {
        float distanceToPlayer2 = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer2 <= chaseRange)
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
