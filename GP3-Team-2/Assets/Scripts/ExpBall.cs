using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    public bool isHealth;
    public bool isStam;
    public bool isDamage;

    void OnTriggerEnter(Collider other)
    {
        StatsInventoryManager stats = other.GetComponent<StatsInventoryManager>();
        if (stats != null)
        {
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
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
