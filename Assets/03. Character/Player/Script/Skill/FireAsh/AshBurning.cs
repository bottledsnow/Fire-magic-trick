using UnityEngine;

public class AshBurning : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CombustiblesObj"))
        {
            CombustiblesObj combustiblesObj = other.GetComponent<CombustiblesObj>();
            combustiblesObj.StartBurning();
        }
    }
}
