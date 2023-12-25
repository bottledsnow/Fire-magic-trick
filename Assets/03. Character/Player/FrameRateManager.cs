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
        SetFrameRate(targetFrameRate);
    }

    public void SetFrameRate(int frameRate)
    {
        QualitySettings.vSyncCount = 0; //關閉垂直同步
        Application.targetFrameRate = frameRate;
        print("FPS調整為" + targetFrameRate);
    } 
}
