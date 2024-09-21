using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionData
{
    public int languageIndex;

    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float voiceVolume;
    public float uiVolume;

    public OptionData(int lanIndex = 2)
    {
        languageIndex = lanIndex;
        masterVolume = 0.5f;
        musicVolume = 0.5f;
        sfxVolume = 0.5f;
        voiceVolume = 1f;
        uiVolume = 1f;
    }
}
