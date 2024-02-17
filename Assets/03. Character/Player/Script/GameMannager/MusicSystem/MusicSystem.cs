using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioSource BackgroundMusic;

    public void PlayMusic()
    {
        BackgroundMusic.Play();
    }
    public void StopMusic()
    {
        BackgroundMusic.Stop();
    }
}
