using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class FireFloat : MonoBehaviour
{
    private ControllerInput _input;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    [SerializeField] private float maxFloatTime;
    [SerializeField] private MMF_Player fireFloat;
    private float timer;
    private bool isTrigger;
    private bool isCheck;
    private bool isTimer;
    private bool needInitialize;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _energySystem = _playerState.GetComponent<EnergySystem>();
    }
    private void Update()
    {
        FloatSystem();
        floatTimer();
        InitializeTrigger();
    }
    private void FloatSystem()
    {
        if(_input.ButtonA && _playerState.canFloat && !_playerState.nearGround)
        {
            EnergyCheck();
        }
        else
        {
            floatEnd();
        }
    }
    private void EnergyCheck()
    {
        if(!isCheck)
        {
            isCheck = true;
            cancelChaeck();

            bool CanUse = false;
            _energySystem.UseFloat(out CanUse);

            if (CanUse)
            {
                floatStart();
            }
        }
    }
    private async void cancelChaeck()
    {
        await Task.Delay(5000);
        isCheck = false;
    }
    private void floatStart()
    {
        if(!isTimer && !isTrigger)
        {
            isTrigger = true;
            isTimer = true;
            _playerState.SetGravityToFloat();
            _playerState.SetIsFloat(true);
            fireFloat.PlayFeedbacks();
        }
    }
    private void floatEnd()
    {
        if(isTimer)
        {
            isTimer = false;
            timer = 0;
            _playerState.SetGravityToNormal();
            _playerState.SetIsFloat(false);
            fireFloat.StopFeedbacks();
            isCheck = false;
        }
    }
    private void floatTimer()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
            if(!needInitialize)
            {
                needInitialize = true;
            }
        }

        if(timer> maxFloatTime)
        {
            floatEnd();
        }
    }
    private void InitializeTrigger()
    {
        if(_playerState.nearGround && needInitialize)
        {
            needInitialize = false;
            isTrigger = false;
            isCheck = false;
        }
    }
    public void ResetFloatingTrigger()
    {
        needInitialize = false;
        isTrigger = false;
        isCheck = false;
    }
}
