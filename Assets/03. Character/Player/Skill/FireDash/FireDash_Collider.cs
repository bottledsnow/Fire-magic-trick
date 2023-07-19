using UnityEngine;
public class FireDash_Collider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CombustiblesObj"))
        {
            CombustiblesObj Combustibles = other.gameObject.GetComponent<CombustiblesObj>();
            Combustibles.Burning_Start();
        }
    }
}
