using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTest : MonoBehaviour
{
    [SerializeField] private bool playBySelf;
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        if (playBySelf)
            PlaySoundEffect();
    }

    public void PlaySoundEffect()
    {

        Debug.Log("Play Sound Effect");
        audioSource.Play();
    }
}