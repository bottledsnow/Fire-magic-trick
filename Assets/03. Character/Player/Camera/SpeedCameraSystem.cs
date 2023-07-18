using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCameraSystem : MonoBehaviour
{
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Start;
    [SerializeField] private MMF_Player Feedbacks_End;

    public void OpenParticle()
    {
        Feedbacks_Start.PlayFeedbacks();
    }
    public void CloseParticle() 
    {
        Feedbacks_End.PlayFeedbacks();
    }
}
