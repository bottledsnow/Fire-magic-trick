using UnityEngine;
using UnityEngine.Video;

public class TriggerArea_Video : MonoBehaviour
{
    private TeachVideo teachVideo;
    private VideoPlayer videoPlayer;

    private void Start()
    {
        teachVideo = GameManager.singleton.UISystem.GetComponent<TeachVideo>();
        videoPlayer = teachVideo.videoPlayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        teachVideo.OnVideoPrepared(videoPlayer);
    }
}
