using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private bool playOnStart;
    [SerializeField] private string bgmName;

    private void Start()
    {
        if (playOnStart)
        {
           PlayBGM();
        }
    }

    public void PlayBGM()
    {
        AudioManager.Instance.PlayBGM(bgmName);
    }
}
