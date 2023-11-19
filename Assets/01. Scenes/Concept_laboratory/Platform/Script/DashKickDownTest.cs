using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashKickDownTest : MonoBehaviour
{
    [SerializeField] private GameObject EnemyA;
    [SerializeField] private Transform[] spawnPositions;

    private GameObject enemy1;
    private GameObject enemy2;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            deleteEnemy();
            spawnEnemy();
        }
    }
    private void spawnEnemy()
    {
        enemy1 = Instantiate(EnemyA, spawnPositions[0].position, spawnPositions[0].rotation);
        enemy2 = Instantiate(EnemyA, spawnPositions[1].position, spawnPositions[1].rotation);
    }
    private void deleteEnemy()
    {
        if (enemy1 != null && enemy2 != null) 
        {
            Destroy(enemy1);
            Destroy(enemy2);
        }
    }
}
