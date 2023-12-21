using UnityEngine;
using System.Threading.Tasks;

public class TriggerDemoEnd : MonoBehaviour
{
    [SerializeField] private Animator BlackImage;
    [SerializeField] private GameObject Text;
    [SerializeField] private Transform StartPosition;

    private bool trigger;
    private Transform player;

    private void Start()
    {
        player = GameManager.singleton.Player;
    }
    public async void DemoEnd()
    {
        if(!trigger)
        {
            trigger = true;
            BlackImage.Play("Enter");
            await Task.Delay(2000);
            Text.SetActive(true);
            await Task.Delay(3000);
            Text.SetActive(false);
            player.transform.position = StartPosition.position;
            BlackImage.Play("Exit");
            trigger = false;
        }
        
    }
}
