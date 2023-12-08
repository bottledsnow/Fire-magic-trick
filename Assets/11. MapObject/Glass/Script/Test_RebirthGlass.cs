using UnityEngine;

public class Test_RebirthGlass : MonoBehaviour
{
    [SerializeField] private GlassSystem[] glassSystem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for (int i = 0; i < glassSystem.Length; i++)
            {
                glassSystem[i].GlassRebirth();
            }
        }
    }

}
