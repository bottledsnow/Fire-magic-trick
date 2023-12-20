using UnityEngine;

public class EnemyB_BulletHitBullet : MonoBehaviour
{
    [SerializeField] private GameObject SpreadArea;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            HitByPlayerBullet(collision);
        }
    }
    private void HitByPlayerBullet(Collision collision)
    {
        SpreadArea.SetActive(true);
        Destroy(this.gameObject, 1.5f);
    }
}
