using UnityEngine;

public class EnemyStoreHouse : MonoBehaviour
{
    private EnemyHealthSystem[] enemys;

    private void Awake()
    {
        enemys = GetComponentsInChildren<EnemyHealthSystem>();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].gameObject.SetActive(false);
        }
    }
    public void CloseAllEnemy()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] != null)
            {
                if (enemys[i].gameObject.activeSelf == true)
                {
                    enemys[i].GetComponent<EnemyHealthSystem>().EnemyDeathRightNow();
                    enemys[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public EnemyHealthSystem GetEnemy()
    {
        for(int i=0;i<enemys.Length;i++)
        {
            if (enemys[i] != null)
            {
                if (enemys[i].gameObject.activeSelf == false)
                {
                    return enemys[i];
                }
            }
        }
        return null;
    }
}
