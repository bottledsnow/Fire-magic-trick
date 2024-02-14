using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TeachVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    void Start()
    {
        // 将 VideoPlayer 的渲染目标设置为 RawImage 的材质
        videoPlayer.targetTexture = new RenderTexture((int)rawImage.rectTransform.rect.width, (int)rawImage.rectTransform.rect.height, 0);
        rawImage.texture = videoPlayer.targetTexture;

        // 准备视频
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }
    public void ChageVideoClip(VideoClip videoClip)
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Prepare();
    }
    public void OnVideoPrepared(VideoPlayer source)
    {
        // 准备完成后，开始播放
        videoPlayer.Play();
    }
}
