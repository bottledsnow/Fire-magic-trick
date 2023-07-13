using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAsh : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private MMFeedbacks AshFeedback;
    [SerializeField] private FireAbsorb Absorb;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        AshFeedback?.Initialization(this.gameObject);
        AshFeedback?.PlayFeedbacks();
    }
    public void AbsorbFire()
    {
        Absorb.enabled = true;
    }
}
