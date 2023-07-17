using UnityEngine;

public class FireDash_Collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CombustiblesObj"))
        {
            CombustiblesObj Combustibles = other.attachedRigidbody.GetComponent<CombustiblesObj>();
            Combustibles.Burning_Start();
        }
    }
}
