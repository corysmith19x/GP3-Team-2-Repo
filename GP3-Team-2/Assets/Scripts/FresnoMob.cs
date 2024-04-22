using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FresnoMob : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    GameObject mainCharacter;

    [Header("Health Parameters")]
    public float enemyHealth;
    public float maxEnemyHealth = 100f;

    Animator animator;


    [Header("Attack Parameters")]
    public float damage = -10f;
    //bool alreadyAttacked = false;
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked = false;

    [Header("Healthbar")]
    public Image health;

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

        ChasePlayer();
        CheckHealth();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void CheckHealth()
    {
        

        if (enemyHealth > maxEnemyHealth)
        {
            enemyHealth = maxEnemyHealth;
        }
        else if(enemyHealth < 0)
        {
            enemyHealth = 0;
        }

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
    

    void OnCollisionEnter(Collision other)
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
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Debug.Log("Enemy Attack CD");
    }
}
