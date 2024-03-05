using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip[] musics;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void playMusic(int index)
    {
        source.Stop();
        source.clip = musics[index];
        source.Play();
    }
}
