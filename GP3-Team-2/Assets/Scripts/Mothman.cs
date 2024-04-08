using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mothman : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    GameObject mainCharacter;

    [Header("Health Parameters")]
    public float health;
    public float maxHealth = 500f;
    bool isCapturable;

    Animator animator;


    [Header("Attack Parameters")]
    public float attackRange = 1f;
    //bool alreadyAttacked = false;
    //public float timeBetweenAttacks;

    private void Awake()
    {
        player = GameObject.Find("player_character_BL_rigged").transform;
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
        if (!isCapturable)
        {
            ChasePlayer();
        }
        else if (isCapturable) 
        {
            animator.SetTrigger("isStunned");
            agent.SetDestination(transform.position);
        }
        if(AttackRangeCheck())
        {
            Attack();
        }
        CheckCapturable();
        CheckHealth();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        //Debug.Log("Attack");
        animator.SetTrigger("attackTrigger");
    }

    private void CheckCapturable()
    {
        if (health <= 100)
        {
            isCapturable = true;
        }
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
            health -= 50f;
        }
        
        if (isCapturable)
        {
            if(other.gameObject.tag == "Net")
            {
                Destroy(gameObject);
            }

            SceneManager.LoadScene("Victory");
        }
    }
}
