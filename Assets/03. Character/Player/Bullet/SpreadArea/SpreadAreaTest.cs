using UnityEngine;
using System.Threading.Tasks;

public class SpreadAreaTest : MonoBehaviour
{
    [SerializeField] private GameObject spreadArea;
    [SerializeField] private int SpreaTime_ms;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlaySpreadArea();
        }
    }
    private async void PlaySpreadArea()
    {
        spreadArea.SetActive(true);
        await Task.Delay(SpreaTime_ms);
        spreadArea.SetActive(false);
    }
}
