using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;

public class Fire_Teleport : MonoBehaviour
{
    private ControllerInput _input;
    private ThirdPersonController _Player;
    private FireCheck_Easy fireCheck;

    private void Update()
    {
        ignit();
    }
    private void Start()
    {
        _input = GameManager.singleton._input;
        _Player = GetComponent<ThirdPersonController>();
        fireCheck = Camera.main.GetComponent<FireCheck_Easy>();
    }
    private void ignit()
    {
        if(_input.RB )
        {
            if(fireCheck.isChooseFirePoint)
            {
                FireTeleprot();
                fireCheck.isChooseFirePoint = false;
                fireCheck.AbsorbFire();
                fireCheck.DestroyFirePoint();
            }
        }
    }
    private async void FireTeleprot()
    {
        _Player.enabled = false;
        //transform.position = fireCheck.gameObject.transform.position;
        transform.position = fireCheck.FirePoint.position;
        await Task.Delay(100);
        _Player.enabled = true;
    }
}
