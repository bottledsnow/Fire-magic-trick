using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeedbacks : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;

    private void Start()
    {
        PlayFeedback();
    }
    public void PlayFeedback()
    {
        Feedbacks.PlayFeedbacks();
    }
}
