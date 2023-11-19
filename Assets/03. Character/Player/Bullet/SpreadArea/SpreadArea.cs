using UnityEngine;

public class SpreadArea : MonoBehaviour
{
    private PlayerDamage playerDamage;

    private void Awake()
    {
        playerDamage = GetComponent<PlayerDamage>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            playerDamage.ToDamageEnemy(other);
        }
    }
}
