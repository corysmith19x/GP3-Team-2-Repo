using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FresnoMob : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    GameObject mainCharacter;

    [Header("Health Parameters")]
    public float health;
    public float maxHealth = 100f;

    Animator animator;


    [Header("Attack Parameters")]
    public float damage = -10f;
    //bool alreadyAttacked = false;
    public float timeBetweenAttacks = 2f;
    bool alreadyAttacked = false;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged Variant").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        CheckHealth();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void CheckHealth()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
    

    void OnCollisionEnter(Collision other)
    {   
        Debug.Log("Enemy att");
        if (other.gameObject.tag == "Bullet")
        {
            health -= 50f;
        }

        if (!alreadyAttacked && other.gameObject.tag == "Player")
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
