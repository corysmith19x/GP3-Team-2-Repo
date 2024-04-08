using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField] private float enemySpawnTimer = 7f;
    [SerializeField] private int howManyEnemies;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Will spawn 10 enemy prefabs every 7 seconds
    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < howManyEnemies; i++)
        {
            WaitForSeconds wait = new WaitForSeconds(enemySpawnTimer);

            yield return new WaitForSeconds(enemySpawnTimer);
        
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            if (i == (howManyEnemies - 1))
            {
                Destroy(gameObject);
            }
        }
    }
}