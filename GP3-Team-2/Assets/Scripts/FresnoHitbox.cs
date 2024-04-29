using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FresnoHitbox : MonoBehaviour
{
    private float damage = -25f;
    bool alreadyAttacked = false;
    public float timeBetweenAttacks;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Attack()
    {
        if (!alreadyAttacked)
        {
            Invoke(nameof(ResetAttack), timeBetweenAttacks * Time.deltaTime);
            LevelStatTracker.instance.DamageTaken(damage);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Debug.Log("Enemy Attack CD");
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Player")
        {
            if (!alreadyAttacked)
            {
                Attack();
                other.gameObject.GetComponent<StatsInventoryManager>().UpdateHealth(damage);
            }
            Debug.Log("Hit");
        }
    }
}
