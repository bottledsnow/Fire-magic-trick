using UnityEngine;

public class EnterArea : MonoBehaviour
{
    [SerializeField] private Transform TargetPositoin;
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = TargetPositoin.position;
        }
    }
}
