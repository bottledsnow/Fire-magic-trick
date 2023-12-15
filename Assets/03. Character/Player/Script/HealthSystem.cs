using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class HealthSystem : MonoBehaviour
{
    public bool Invincible;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks;
    [Header("Invicible")]
    [SerializeField] private float HitInvicibleTime = 0.5f;



    private EnergySystem energySystem;
    private ImpactReceiver impactReceiver;

    private void Start()
    {
        energySystem = GameManager.singleton.Player.GetComponent<EnergySystem>();
        impactReceiver = GameManager.singleton.Player.GetComponent<ImpactReceiver>();
    }
    public void ToPushPlayer(Vector3 ImpactDirection)
    {
        toPushPlayer(ImpactDirection);
    }
    public void ToDamagePlayer(int Damage)
    {
        if(!Invincible)
        {
            DamagePlayer(Damage);
        }
    }
    public void ToDamagePlayer(int Damage,Vector3 ImpactDirection)
    {
        if(!Invincible)
        {
            DamagePlayer(Damage, ImpactDirection);
        }
    }
    private void toPushPlayer(Vector3 ImpactDirection)
    {
        ToImpactPlayer(ImpactDirection);
    }
    private void DamagePlayer(int Damage)
    {
        energySystem.DecreaseEnergy(Damage);
        HitInvincible();
        PlayFeedbacks();
    }
    private void DamagePlayer(int Damage, Vector3 ImpactDirection)
    {
        DamagePlayer(Damage);
        ToImpactPlayer(ImpactDirection);
    }
    public void ToImpactPlayer(Vector3 Direction)
    {
        impactReceiver.AddImpact(Direction);
    }
    private void PlayFeedbacks()
    {
        Feedbacks.PlayFeedbacks();
    }
    private async void HitInvincible()
    {
        SetInvincible(true);
        await Task.Delay((int)(HitInvicibleTime*1000));
        SetInvincible(false);
    }
    private void SetInvincible(bool Active)
    {
        Invincible = Active;
    }
}
