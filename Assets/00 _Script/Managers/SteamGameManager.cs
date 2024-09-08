using MoreMountains.Feedbacks;
using Steamworks;
using UnityEngine;

public class SteamGameManager : MonoBehaviour
{
    private void Start()
    {
        if(SteamManager.Initialized)
        {
            Debug.Log("SteamManager is initialized");
        }
        else
        {
            Debug.Log("SteamManager is not initialized");
            return;
        }

    }

    [ContextMenu("Unlock Achievement")]
    public void UnlockAchievement()
    {
        if (SteamManager.Initialized)
        {
            SteamUserStats.SetAchievement("DEV_Button");
            SteamUserStats.StoreStats();
        }
    }
}
