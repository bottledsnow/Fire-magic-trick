using UnityEngine;

public class RebirthArea : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(Enemy, spawnPosition.position, spawnPosition.rotation);
        }
    }
}
