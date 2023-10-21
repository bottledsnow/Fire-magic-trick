using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UIElements;

public class Lego : MonoBehaviour
{
    [SerializeField] private MMF_Player Piller;
    [SerializeField] private MMF_Player Box01;
    [SerializeField] private MMF_Player Box02;
    [SerializeField] private int Duration = 5;

    private void Start()
    {
        rotationPiller();
    }
    private void Update()
    {
        
    }
    private async void rotationPiller()
    {
        while (true)
        {
            Piller.PlayFeedbacks();
            Box01.PlayFeedbacks();
            Box02.PlayFeedbacks();
            await Task.Delay(Duration * 1000);
        }
    }
}

