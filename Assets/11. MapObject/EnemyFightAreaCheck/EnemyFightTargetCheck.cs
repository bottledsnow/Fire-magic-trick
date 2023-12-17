using UnityEngine;

public class EnemyFightTargetCheck : MonoBehaviour
{
    [SerializeField] private bool onceTrigger;
    [Header("Target")]
    [SerializeField] private GameObject EnemyTarget;
    [SerializeField] private GameObject[] noEnemyTriggerTargets;

    private bool Trigger;

    private void Update()
    {
        Check();
    }
    private void Check()
    {
        if(EnemyTarget.activeSelf==false)
        {
            for(int i = 0; i < noEnemyTriggerTargets.Length; i++)
            {
                noEnemyTriggerTargets[i].SetActive(false);
            }
        }
    }
}
