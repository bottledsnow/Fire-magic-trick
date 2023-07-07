using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAsh : MonoBehaviour,IFirePoint
{
    [SerializeField] MMFeedbacks AshFeedback;
    [SerializeField] private FireAbsorb Absorb;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject FirePoint;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        AshFeedback?.Initialization(this.gameObject);
        AshFeedback?.PlayFeedbacks();
    }

    public void PlayerChoosePoint()
    {
        Debug.Log("Hit Point");
        if(FirePoint != null )
            FirePoint.SetActive(true);
    }
    public void PlayerNotChoosePoint()
    {
        Debug.Log("Leave Point");
        if(FirePoint != null )
        FirePoint.SetActive(false);
    }
    public void DestroyChoosePoint()
    {
        Destroy(FirePoint);
    }
    public void AbsorbFire()
    {
        Absorb.enabled = true;
    }
}
