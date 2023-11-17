using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class OrganizeNearbyEnemy : Action
{
	[Header("SharedVariable")]
	[SerializeField] private SharedGameObjectList teammateEnemies;

	[Header("DetectionRadius")]
	[SerializeField] private float detectionRadius;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 currentPosition = transform.position;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        teammateEnemies.Value.Clear();

        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject)
            {
                float distance = Vector3.Distance(currentPosition, enemy.transform.position);

                if (distance < detectionRadius)
                {
					EnemyHealthSystem enemyHealthSystem = enemy.GetComponent<EnemyHealthSystem>();
					if(enemyHealthSystem != null)
					{
                    	teammateEnemies.Value.Add(enemy);
					}
                }
            }
        }

        return TaskStatus.Success;

	}
}