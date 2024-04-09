using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarProjectile : MonoBehaviour
{
    private float damage = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatsInventoryManager>().UpdateHealth(damage);
            LevelStatTracker.instance.DamageTaken(damage);
            Destroy(gameObject);
        }
    }
}
