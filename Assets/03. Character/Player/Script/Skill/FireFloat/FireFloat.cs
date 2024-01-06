using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class FireFloat : MonoBehaviour
{
    [SerializeField] private float maxFloatTime;

    //delegate
    public delegate void OnFloatStartHandler();
    public delegate void OnFloatEndHandler();
    public event OnFloatStartHandler OnFloatStart;
    public event OnFloatEndHandler OnFloatEnd;

    //script
    private ControllerInput _input;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    private ParticleSystem vfx_float;

    //variable
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
        vfx_float = GameManager.singleton.VFX_List.VFX_Float;
    }
    private void Update()
    {
        FloatSystem();
        floatTimer();
        InitializeTrigger();
    }
    private void FloatSystem()
    {
        if(_input.ButtonA && _playerState.canFloat)
        {
            if(!_playerState.nearGround)
            {
                EnergyCheck();
            }
        }
        else
        {
            if (_playerState.isGround || !_input.ButtonA)
            {
                floatEnd();
            }
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
            vfx_float.Play();
            OnFloatStart?.Invoke();
        }
    }
    private void floatEnd()
    {
        if(isTimer)
        {
            Debug.Log("Float End");
            isTimer = false;
            timer = 0;
            _playerState.SetGravityToNormal();
            _playerState.SetIsFloat(false);
            vfx_float.Stop();
            isCheck = false;
            OnFloatEnd?.Invoke();
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
