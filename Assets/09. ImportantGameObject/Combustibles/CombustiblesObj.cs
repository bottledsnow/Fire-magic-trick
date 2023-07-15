using MoreMountains.Feedbacks;
using UnityEngine;

public class CombustiblesObj : MonoBehaviour
{
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Burning;
    private void Start()
    {
        StartBurning();
    }
    private void StartBurning()
    {
        Feedbacks_Burning.PlayFeedbacks();
    }
}
