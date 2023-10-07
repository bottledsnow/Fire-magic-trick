using MoreMountains.Feedbacks;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [SerializeField] private MMF_Player Timescale;
    private void Update()
    {
        TimeScaleSystem();
    }
    private void TimeScaleSystem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetTimeScale(0);
            Debug.Log("TimeScale 0");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTimeScale(0.25f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTimeScale(0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTimeScale(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetTimeScale(2);
        }
    }
    private void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
