using UnityEngine;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class GlassSystem : MonoBehaviour
{
    public enum Mode
    {
        Fast,
        Delay,
        Charge,
        Crash
    }
    public Mode mode;
    [Header("UniversalFeedbacks")]
    [SerializeField] private MMF_Player feedbacks_Broken;
    [Header("FastMode")]
    [SerializeField] private float fastTime;
    [Header("DelayMode")]
    [SerializeField] private MMF_Player feedbacks_Delay;
    [SerializeField] private float delayTime;


    private Collider glassCollider;
    private MeshRenderer glassRender;
    private bool isBroken;

    private void Awake()
    {
        glassCollider = GetComponent<Collider>();
        glassRender = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Broken();
        }
    }
    public void Broken()
    {
        if(!isBroken)
        {
            switch (mode)
            {
                case Mode.Fast:
                    fastMode();
                    break;
                case Mode.Delay:
                    delayMode();
                    break;
                case Mode.Charge:
                    chargeMode();
                    break;
                case Mode.Crash:
                    crashMode();
                    break;
            }
            SetIsBroken(true);
        }
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
    private void crashMode()
    {
        Debug.Log("crashMode");
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
    private void SetIsBroken(bool active)
    {
        isBroken = active;
    }
    private void SetCollider(bool active)
    {
        glassCollider.enabled = active;
    }
    private void SetColliderRender(bool active)
    {
        glassRender.enabled = active;
    }
}