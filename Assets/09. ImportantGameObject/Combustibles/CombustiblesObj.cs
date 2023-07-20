using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class CombustiblesObj : MonoBehaviour
{
    private BurningSystem burningSystem;
    [Header("public")]
    public bool burning = false;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_Burning_Start;
    [SerializeField] private MMF_Player Feedbacks_Burning_Keep;
    [SerializeField] private MMF_Player Feedbacks_Burning_End;

    private int DamageCount;
    private void Start()
    {
        
        burningSystem = GameManager.singleton._input.GetComponent<BurningSystem>();
    }
    public void StartBurning()
    {
        if(!burning)
        {
            burning = true;
            Feedbacks_Burning_Start.PlayFeedbacks();
            FireDuration();
        }
    }
    private async void FireDuration()
    {
        DamageCount = (int)burningSystem.DamageCount;
        for (int i = 0; i < DamageCount; i++)
        {
            await Task.Delay((int)burningSystem.BurningInterval_ms);
            Burning_Keep();
        }
        Burning_End();
    }
    private void Burning_Keep()
    {
        if (Feedbacks_Burning_Keep != null)
            Feedbacks_Burning_Keep.PlayFeedbacks();
    }
    private void Burning_End()
    {
        if(Feedbacks_Burning_End != null)
        {
            Feedbacks_Burning_End.PlayFeedbacks();
            Feedbacks_Burning_Start.StopFeedbacks();
            burning = false;
        }
    }
}
