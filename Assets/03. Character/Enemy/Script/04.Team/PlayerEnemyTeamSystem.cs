using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class PlayerEnemyTeamSystem : MonoBehaviour
{
    [Header("AttackingEnemyArray")]
    [SerializeField] private GameObject[] meleeEnemy;
    [SerializeField] private GameObject[] rangedEnemy;

    [Header("MaxAttackingEnemy")]
    [SerializeField] private float maxMeleeEnemies;
    [SerializeField] private float maxRangedEnemies;

    [Header("EnemyDetectRange")]
    [SerializeField] private float meleeDetectionRadius;
    [SerializeField] private float rangedDetectionRadius;

    GameObject[] enemies;

    void Start()
    {

    }


    void Update()
    {
        GetAllEnemy();
        InstantiateEnemyTeam();
    }

    void InstantiateEnemyTeam()
    {
        meleeEnemy = DetectNearbyEnemy(meleeDetectionRadius, "Melee", maxMeleeEnemies);
        rangedEnemy = DetectNearbyEnemy(rangedDetectionRadius, "Ranged", maxRangedEnemies);
    }

    void GetAllEnemy()
    {
        GameObject[] enemyTagObject = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> allEnemies = new List<GameObject>();

        foreach (GameObject gameObject in enemyTagObject)
        {
            EnemyTeamSystem enemyTeamSystem = gameObject.GetComponent<EnemyTeamSystem>();
            if (enemyTeamSystem != null)
            {
                allEnemies.Add(gameObject);
            }
        }
        enemies = allEnemies.ToArray();
    }

    GameObject[] DetectNearbyEnemy(float detectionRadius, string attackingStyle, float maxEnemies)
    {
        Vector3 currentPosition = transform.position;

        List<GameObject> nearbyEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            EnemyTeamSystem enemyTeamSystem = enemy.GetComponent<EnemyTeamSystem>();

            if (enemyTeamSystem.attackingStyle.ToString() == attackingStyle)
            {
                if (distance < detectionRadius && nearbyEnemies.Count < maxEnemies)
                {
                    if (nearbyEnemies.Count < maxEnemies)
                    {
                        nearbyEnemies.Add(enemy);
                        AttackPermissionsController(enemy, true);
                    }
                }
                else
                {
                    AttackPermissionsController(enemy, false);
                }
            }
        }
        return nearbyEnemies.ToArray();
    }

    void AttackPermissionsController(GameObject enemy, bool isTrue)
    {
        EnemyTeamSystem enemyTeamSystem = enemy.GetComponent<EnemyTeamSystem>();
        if (enemyTeamSystem != null)
        {
            BehaviorTree behaviorTree = enemy.GetComponent<BehaviorTree>();
            behaviorTree.SetVariableValue("attackingPermissions", isTrue);
        }
    }
}
