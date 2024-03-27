using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    float enemyHealth; 
    float maxEnemyHealth;
    bool isCapturable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(isCapturable)
        {
            if (other.gameObject.tag == "Net")
            {
                Destroy(gameObject);
            }
        }
    }
}
