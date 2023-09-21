using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class PrototypeN4 : MonoBehaviour
{
    [SerializeField] private GameObject Trigger;
    [SerializeField] private MMF_Player Feedbacks;
    [SerializeField] private BrokeGlassN4[] BrokeGlassN4s;
    private ControllerInput _input;
    public bool isTrigger;
    private int BrokenNumber;

    private void Start()
    {
        _input = GameManager.singleton._input;
    }

    private void Update()
    {
        CheckSuperDashButton();
    }
    private void CheckSuperDashButton()
    {
        if(_input.LB && !isTrigger)
        {
            N4();
            isTrigger = true;   
        }
    }
    private void N4()
    {
        Feedbacks.StopFeedbacks();
    }
    public async void N4GlassBrokenStart()
    {
        for(int i=0;i<BrokeGlassN4s.Length;i++)
        {
            BrokeGlassN4s[i].Broke();
            await Task.Delay(1000);
        }
    }
}
