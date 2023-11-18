using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyTeamSystem : MonoBehaviour
{
    [Header("EnemyType")]
    [SerializeField] private EnemyType enemyType;

    [Header("DetectNearbyEnemies")]
	[SerializeField] private float detectionRadius = 15;

    public enum EnemyType{A,B,C,Connie}

    private BehaviorTree bt;
    private List<GameObject> nearbyEnemies;

    void Start()
    {
        bt = GetComponent<BehaviorTree>();
    }

    void Update()
    {
        SetTeammate();
    }

    void SetTeammate()
    {
        nearbyEnemies = DetectNearbyEnemies();
        bt.SetVariableValue("teammateEnemies" , nearbyEnemies);
    }

    List<GameObject> DetectNearbyEnemies()
    {
        Vector3 currentPosition = transform.position;

        List<GameObject> nearbyEnemies = new List<GameObject>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject)
            {
                float distance = Vector3.Distance(currentPosition, enemy.transform.position);

                if (distance < detectionRadius)
                {
					EnemyTeamSystem enemyTeamSystem = enemy.GetComponent<EnemyTeamSystem>();
					if(enemyTeamSystem != null)
					{
                    	nearbyEnemies.Add(enemy);
					}
                }
            }
        }

        return nearbyEnemies;
    }
}
