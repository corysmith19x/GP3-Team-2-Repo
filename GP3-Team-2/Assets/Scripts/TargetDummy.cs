using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    [SerializeField] private float enemyHealth; 
    public float maxEnemyHealth = 5000f;
    bool isCapturable = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
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

    void OnCollisionEnter(Collision other)
    {
        if(isCapturable)
        {
            if (other.gameObject.tag == "Net")
            {
                //Play animation

                //Destroy
                Destroy(gameObject);
            }
        }

        if (other.gameObject.tag == "Bullet")
        {
            enemyHealth -= 20f;
            Debug.Log("Enemy health is " + enemyHealth);
        }
    }
}
