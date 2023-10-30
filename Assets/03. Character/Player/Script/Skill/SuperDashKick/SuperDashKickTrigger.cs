using UnityEngine;
using System.Threading.Tasks;

public class SuperDashKickTrigger : MonoBehaviour
{
    private bool isKick;
    private void OnTriggerEnter(Collider other)
    {
        if(isKick)
        {
            if (other.CompareTag("Enemy"))
            {
                PassCollider(other);
            }
        }
    }
    public async void PassCollider(Collider colider)
    {
        colider.enabled = false;
        await Task.Delay(1000);
        colider.enabled = true;
    }
    public async void TriggerKick()
    {
        isKick = true;
        await Task.Delay(1000);
        isKick = false;
    }
}
