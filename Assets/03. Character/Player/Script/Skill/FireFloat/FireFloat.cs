using MoreMountains.Feedbacks;
using UnityEngine;

public class FireFloat : MonoBehaviour
{
    private ControllerInput _input;
    private PlayerState _playerState;

    [SerializeField] private float maxFloatTime;
    [SerializeField] private MMF_Player fireFloat;
    private float timer;
    private bool isTrigger;
    private bool isTimer;
    private bool needInitialize;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
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
            floatStart();
        }
        else
        {
            floatEnd();
        }
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
            Debug.Log("float");
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
            Debug.Log("end");
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
            isTrigger = false;
            needInitialize = false;
        }
    }
}
