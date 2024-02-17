using UnityEngine;

public class BulletDeadSpace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
