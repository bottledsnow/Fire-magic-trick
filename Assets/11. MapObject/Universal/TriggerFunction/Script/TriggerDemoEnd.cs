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
            BlackImage.SetTrigger("Enter");
            await Task.Delay(3000);
            BlackImage.SetTrigger("Exit");
            player.transform.position = StartPosition.position;
            trigger = false;
        }
    }
}
