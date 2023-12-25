using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [Header("FPSSetting")]
    [Range(30,240)]
    [SerializeField] private int targetFrameRate = 30;

    void Start()
    {
        
    }

    public void SetFrameRate()
    {
        QualitySettings.vSyncCount = 0; //關閉垂直同步
        Application.targetFrameRate = targetFrameRate;
        print("Set FPS to " + targetFrameRate);
    } 
}
