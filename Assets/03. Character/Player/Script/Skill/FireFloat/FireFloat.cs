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
    private ControllerInput input;
    private EnergySystem energySystem;
    private PlayerState playerState;
    private ParticleSystem vfx_float;
    private NGP_SuperJump superJump;

    //variable
    private float timer;
    private bool isTrigger;
    private bool isCheck;
    private bool isTimer;
    private bool needInitialize;

    private void Start()
    {
        playerState = GameManager.singleton._playerState;
        input = GameManager.singleton._input;
        vfx_float = GameManager.singleton.VFX_List.VFX_Float;
        energySystem = playerState.GetComponent<EnergySystem>();
        superJump = GameManager.singleton.NewGamePlay.GetComponent<NGP_SuperJump>();
    }
    private void Update()
    {
        FloatSystem();
        floatTimer();
        InitializeTrigger();
    }
    private void FloatSystem()
    {
        if(input.ButtonA && playerState.canFloat)
        {
            if(!playerState.nearGround)
            {
                EnergyCheck();
            }
        }
        else
        {
            if (playerState.isGround || !input.ButtonA)
            {
                floatEnd();
            }
        }
    }
    private void EnergyCheck()
    {
        if (!isTimer && !isTrigger)
        {
            if (!isCheck)
            {
                isCheck = true;
                cancelChaeck();

                if (energySystem.canUseEnegy(EnergySystem.SkillType.Float))
                {
                    floatStart();
                }
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

        isTrigger = true;
        isTimer = true;
        playerState.SetGravityToFloat();
        playerState.SetIsFloat(true);
        vfx_float.Play();
        OnFloatStart?.Invoke();

    }
    private void floatEnd()
    {
        if (!superJump.isHeavy)
        {
            if (isTimer)
            {
                isTimer = false;
                timer = 0;
                playerState.SetGravityToNormal();
                playerState.SetIsFloat(false);
                vfx_float.Stop();
                isCheck = false;
                OnFloatEnd?.Invoke();
            }
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
        if(playerState.nearGround && needInitialize)
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
