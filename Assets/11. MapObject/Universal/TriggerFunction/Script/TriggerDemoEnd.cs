using UnityEngine;
using System.Threading.Tasks;

public class TriggerDemoEnd : MonoBehaviour
{
    //Script
    [SerializeField] private Animator BlackImage;
    [SerializeField] private GameObject Text;
    [SerializeField] private Transform StartPosition;
    private SenceManagerment senceManagerment;

    //variable
    private bool trigger;
    private void Start()
    {
        senceManagerment = GameManager.singleton.GetComponent<SenceManagerment>();
    }
    public async void DemoEnd()
    {
        if(!trigger)
        {
            trigger = true;
            BlackImage.SetTrigger("Enter");
            await Task.Delay(3000);
            senceManagerment.ReStartGame();
        }
    }
}
