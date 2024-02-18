using UnityEngine;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class GlassSystem : MonoBehaviour
{
    public enum Mode
    {
        Fast,
        Delay,
        unlimited
    }
    public Mode mode;
    [Header("UniversalFeedbacks")]
    [SerializeField] private MMF_Player feedbacks_Broken;
    [Header("FastMode")]
    [SerializeField] private float fastTime;
    [Header("DelayMode")]
    [SerializeField] private MMF_Player feedbacks_Delay;
    [SerializeField] private float delayTime;
    [Header("Charge")]
    [SerializeField] private bool canCharge;
    [Header("Crash")]
    [SerializeField] private bool canCrash;
    [Header("EnemyCrash")]
    [SerializeField] private bool canEnemyCrash;
    [Header("SuperJump")]
    [SerializeField] private bool canSuperJump;


    private ProgressSystem progressSystem;
    private Collider glassCollider;
    private MeshRenderer glassRender;
    private bool isBroken;

    private void Awake()
    {
        glassCollider = GetComponent<Collider>();
        glassRender = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
        progressSystem.OnPlayerDeath += OnPlayerDeathToRebirthGlass;
    }
    private void OnCollisionEnter(Collision collision)
    {
        BrokenCheck_Charge(collision);
        BrokenCheck_EnemyCrash(collision);
    }
    private void OnPlayerDeathToRebirthGlass()
    {
          GlassRebirth();
    }
    private void BrokenCheck_Charge(Collision collision)
    {
        if(canCharge)
        {
            if (collision.collider.CompareTag("ChargeBullet"))
            {
                BrokenSuperFast();
            }
        }
    }
    public void BrokenCheck_SuperJump()
    {
        if(canSuperJump)
        {
            BrokenSuperFast();
        }
    }
    public void BrokenCheck_Crash()
    {
        if (canCrash)
        {
            BrokenSuperFast();
        }
    }
    private void BrokenCheck_EnemyCrash(Collision collision)
    {
        if(canEnemyCrash)
        {
            if(collision.collider.CompareTag("Enemy"))
            {
                EnemyHealthSystem enemy = collision.collider.GetComponent<EnemyHealthSystem>();
                if(enemy.atCrash)
                {
                    BrokenSuperFast();
                }
            }
        }
    }
    public void Broken()
    {
        if(!isBroken && mode != Mode.unlimited)
        {
            switch (mode)
            {
                case Mode.Fast:
                    fastMode();
                    break;
                case Mode.Delay:
                    delayMode();
                    break;
            }

            SetIsBroken(true);
        }
    }
    public void  BrokenSuperFast()
    {
        SetGlass(false);
    }
    public void GlassRebirth()
    {
        SetIsBroken(false);
        SetGlass(true);
    }
    private async void fastMode()
    {
        await Task.Delay((int)(fastTime * 1000));
        SetGlass(false);

        Debug.Log("fastMode");
    }
    private async void delayMode()
    {
        feedbacks_Delay.PlayFeedbacks();
        await Task.Delay((int)(delayTime * 1000));
        SetGlass(false);

        Debug.Log("delayMode");
    }
    private void chargeMode()
    {
        Debug.Log("chargeMode");
    }
    private void SetGlass(bool active)
    {
        SetCollider(active);
        SetColliderRender(active);

        if(!active)
        {
            feedbacks_Broken.PlayFeedbacks();
        }
    }
    public void QuickSetGlassFalse()
    {
        SetCollider(false);
        SetColliderRender(false);
    }
    private void SetIsBroken(bool active)
    {
        isBroken = active;
        Debug.Log("isBroken : " + active);
    }
    private void SetCollider(bool active)
    {
        glassCollider.enabled = active;
    }
    private void SetColliderRender(bool active)
    {
        glassRender.enabled = active;
    }
    private void SetCanSuperJump(bool active)
    {
        canSuperJump = active;
    }
}