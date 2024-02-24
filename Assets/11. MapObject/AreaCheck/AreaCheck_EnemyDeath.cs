using UnityEngine;
using UnityEngine.Events;

public class AreaCheck_EnemyDeath : MonoBehaviour
{
    public UnityEvent OnEnemyDeath;
    public GameObject[] Enemys;

    private void Update()
    {
        Check();
    }
    private void Check()
    {
        for(int i = 0; i < Enemys.Length; i++)
        {
            if (Enemys[i] != null)
            {
                if (Enemys[i].activeSelf == true)
                {
                    return;
                }
                OnEnemyDeath?.Invoke();
            }
        }
    }
}
