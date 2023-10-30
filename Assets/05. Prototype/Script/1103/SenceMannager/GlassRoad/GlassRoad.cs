using UnityEngine;   
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class GlassRoad : MonoBehaviour
{
    [SerializeField] private BrokeGlassN4[] BrokeGlassN4s;
    private MMF_Player Feedbacks;

    private void Start()
    {
        Feedbacks = GetComponent<MMF_Player>();
    }

    public async void GlassBrokenStart()
    {
        for (int i = 0; i < BrokeGlassN4s.Length; i++)
        {
            BrokeGlassN4s[i].Broke();
            Feedbacks.PlayFeedbacks();
            await Task.Delay(1000);
        }
    }
}
