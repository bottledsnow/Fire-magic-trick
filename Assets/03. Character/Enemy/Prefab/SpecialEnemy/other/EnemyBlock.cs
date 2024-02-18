using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Block;

    private bool isBlock = true;
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        if(Enemy == null || Enemy.activeSelf == false)
        {
            if(isBlock)
            {
                isBlock = false;
                Block.SetActive(false);
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
