using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    /*public bool isHealth;
    public bool isStam;
    public bool isDamage;*/

    //int buffRandom;

    void Start()
    {
        //buffRandom = Random.Range(1, 3);
    }

    void OnTriggerEnter(Collider other)
    {
        StatsInventoryManager stats = other.GetComponent<StatsInventoryManager>();
        if (stats != null)
        {
            stats.characterExp += 1;
            stats.CheckExp();
            Destroy(transform.parent.gameObject);
        }
    }
}

/* Nathan's old code
            if (isHealth)
            {
                stats.healthExp += 1;
                stats.CheckHealthExp();
                Debug.Log("Health EXP Boosted");
            }

            if (isStam)
            {
                stats.stamExp += 1;
                stats.CheckStamExp();
                Debug.Log("Stam EXP Boosted");
            }

            if (isDamage)
            {
                stats.damageExp += 1;
                stats.CheckDamageExp();
                Debug.Log("Damage EXP Boosted");
            }*/

// Unused randomizer Code
/*if (buffRandom == 1)
{
    stats.healthExp += 1;
    stats.CheckHealthExp();
    Debug.Log("Health EXP Boosted");
}

if (buffRandom == 2)
{
    stats.stamExp += 1;
    stats.CheckStamExp();
    Debug.Log("Stam EXP Boosted");
}

if (buffRandom == 3)
{
    stats.damageExp += 1;
    stats.CheckDamageExp();
    Debug.Log("Damage EXP Boosted");
}

//Destroy(transform.parent.gameObject);
Destroy(gameObject);*/
