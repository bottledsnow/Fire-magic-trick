using UnityEngine;

public class PlayerToPlatform : MonoBehaviour
{
    [SerializeField] private MovePlatform movePlatform;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(movePlatform.isMove)
            {
                other.transform.position += movePlatform.updateMove;
            }
        }
    }
}
