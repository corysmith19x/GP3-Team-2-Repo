using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothman : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
        if(AttackRangeCheck())
        {
            Attack();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("attackTrigger");
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
}
