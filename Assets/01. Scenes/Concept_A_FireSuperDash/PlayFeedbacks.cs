using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeedbacks : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;

    public void PlayFeedback()
    {
        Feedbacks.Initialization();
        Feedbacks.PlayFeedbacks();
    }
}
