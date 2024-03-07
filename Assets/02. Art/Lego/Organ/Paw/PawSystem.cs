using UnityEngine;

public class PawSystem : MonoBehaviour
{
    [SerializeField] private Animator animator_paw;
    [SerializeField] private EnemyStoreHouse enemyStoreHouse;
    [SerializeField] private GameObject SpawnPoint;
    private Animator animator;

    //each Enemy
    private EnemyHealthSystem enemy;
    private Enemy_ActiveSystem enemyactive;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void StartPawSystem()
    {
        enemyStoreHouse.CloseAllEnemy();
        GiveNewTarget(0);
    }
    public void StopSystem()
    {
        GiveNewTarget(-1);
        animator.Play("Point_0_Up");
        enemyStoreHouse.CloseAllEnemy();
    }
    public void Catch()
    {
        //animator
        animator_paw.SetBool("isCatch",true);
        GiveNewTarget(-1);
        GiveRandomTarget(animator);

        //Enemy spawn
        enemy = enemyStoreHouse.GetEnemy();

        if(enemy !=null)
        {
            enemy.gameObject.SetActive(true);
            enemy.Rebirth(SpawnPoint.transform.position, Quaternion.identity);
            enemy.transform.parent = SpawnPoint.transform;

            //Enemy active
            enemyactive = enemy.GetComponent<Enemy_ActiveSystem>();
            enemyactive.stopEnemyAll();
        }else
        {
            Debug.Log("NoEnemy");
        }
    }
    public void Freed()
    {
        //animator
        animator_paw.SetBool("isCatch", false);
        GiveNewTarget(0);

        //Enemy active
        if (enemy != null)
        {
            enemy.transform.parent = null;
        }
        if(enemyactive !=null)
        {
            enemyactive.activeEnemy();
        }
    }
    private void GiveRandomTarget(Animator animator)
    {
        int targetIndex = Random.Range(1,4);

        switch(targetIndex)
        {
            case 0:
                GiveNewTarget(0);
                break;
            case 1:
                GiveNewTarget(1);
                break;
            case 2:
                GiveNewTarget(2);
                break;
            case 3:
                GiveNewTarget(3);
                break;
            case 4:
                GiveNewTarget(4);
                break;
        }
    }
    private void GiveNewTarget(int index)
    {
        switch (index)
        {
            case -1:
                animator.SetBool("Go0", false);
                animator.SetBool("Go1", false);
                animator.SetBool("Go2", false);
                animator.SetBool("Go3", false);
                animator.SetBool("Go4", false);
                break;
            case 0:
                animator.SetBool("Go0", true);
                break;
            case 1:
                animator.SetBool("Go1", true);
                break;
            case 2:
                animator.SetBool("Go2", true);
                break;
            case 3:
                animator.SetBool("Go3", true);
                break;
            case 4:
                animator.SetBool("Go4", true);
                break;
        }
    }
}
