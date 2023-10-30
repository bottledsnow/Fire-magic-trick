using UnityEngine;
using System.Threading.Tasks;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] private int Second;
    [SerializeField] private BrokeGlassN8 BrokenGlass;
    [SerializeField] private GameObject Enemys;
    [SerializeField] private GameObject FlyGlass;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartAreaRebirth();
        }
    }
    private async void StartAreaRebirth()
    {
        await Task.Delay(1000 * Second);
        BrokenGlass.Broke();
        FlyGlass.SetActive(true);

        await Task.Delay(5000);
        Enemys.gameObject.SetActive(false);
        BrokenGlass.gameObject.SetActive(false);
        FlyGlass.gameObject.SetActive(false);
    }
}
