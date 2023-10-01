using UnityEngine;

public class TestDashHit : MonoBehaviour
{
    public Rigidbody Enemy;
    public Collider playerController;

    public float crashSpeed;
    private void Update()
    {
        DashHit();
    }
    private void DashHit()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 playerposition = playerController.transform.position;
            Vector3 EnemyPosition = Enemy.transform.position;
            Vector3 direction = (EnemyPosition - playerposition).normalized;
            Enemy.velocity = direction * crashSpeed;
        }

    }
}
