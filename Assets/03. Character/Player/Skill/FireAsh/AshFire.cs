using MoreMountains.Feedbacks;
using UnityEngine;

public class AshFire : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private MMF_Player AshFeedback;
    [SerializeField] private FireAbsorb Absorb;
    private void Start()
    {
        AshFeedback?.Initialization(this.gameObject);
        AshFeedback?.PlayFeedbacks();
        Destroy(gameObject, lifeTime);
    }
    public void AbsorbFire()
    {
        Absorb.enabled = true;
    }
}
