using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Block;

    [SerializeField] private bool isOnce = false;
    private bool isBlock = true;
    private bool isTrigger = false;
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        if(isOnce && isTrigger)
        {
            return;
        }
        if(Enemy == null || Enemy.activeSelf == false)
        {
            if(isBlock)
            {
                isBlock = false;
                Block.SetActive(false);
                isTrigger = true;
            }
        }
        else
        {
            if (!isBlock)
            {
                isBlock = true;
                Block.SetActive(true);
            }
        }
    }
}
