using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Video;

[System.Serializable]
public class TeachSystem_content
{
    public string title;
    public VideoClip video;
    [TextArea(3,10)]public string[] content;
}
public class TeachSystem : MonoBehaviour
{
    [Header("Input To TMP")]
    [SerializeField][TextArea(5, 10)] private string debugText;
    [Header("Setting")]
    [SerializeField] private GameObject teachBar;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI content;
    [Header("Content")]
    public TeachSystem_content[] teachSystem_Content;

    //Script
    [SerializeField] private GameObject[] Script;
    private ControllerInput input;
    private PlayerState playerState;
    private HealthSystem healthSystem;
    private TeachVideo teachVideo;

    //variable
    private int index = 0;
    private int contentIndex = 0;
    private bool isTeach = false;
    private bool canNext = true;

    private void Awake()
    {
        teachVideo = GetComponent<TeachVideo>();
    }
    private void Start()
    {
        input = GameManager.singleton._input;
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        healthSystem = GameManager.singleton.Player.GetComponent<HealthSystem>();

        //Initialization
        setTeachBar(false);
    }
    private void Update()
    {
        teachSystem();
    }
    private void teachSystem()
    {
        if(input !=null)
        {
            if(isTeach)
            {
                if (input.leftClick)
                {
                    if(canNext)
                    {
                        DisplayNextSentence();
                    }
                }
                if (input.cancel)
                {
                    //pauseSystem.StopPause();
                    CloseTeach();
                }
            }
        }
    }
    public void OpenTeach(int index)
    {
        SetIsTeach(true);
        this.index = index;
        contentIndex = 0;
        
        playerState.OutControl_Dialogue();
        playerState.SetUseCameraRotate(false);
        healthSystem.SetStoryInvincible(true);

        //UI
        setTeachBar(true);
        setScriptActiv(false);
        //pauseSystem.Pause();

        //title
        title.text = teachSystem_Content[index].title;

        // video
        teachVideo.ChageVideoClip(teachSystem_Content[index].video);
        teachVideo.videoPlayer.prepareCompleted += OnVideoPrepared;
        teachVideo.videoPlayer.Prepare();
        teachVideo.videoPlayer.Prepare();

        //content
        StopAllCoroutines();
        StartCoroutine(TypeWord(teachSystem_Content[index].content[contentIndex]));
    }
    public void CloseTeach()
    {
        SetIsTeach(false);
        setTeachBar(false);
        setScriptActiv(true);

        healthSystem.SetStoryInvincible(false);
        playerState.SetUseCameraRotate(true);
        playerState.TakeControl_Dialogue();
    }
    public void DisplayNextSentence()
    {
        if (teachSystem_Content[index].content.Length == contentIndex+1)
        {
            CloseTeach();
            return;
        }

        ToNextTimerCooling();

        contentIndex++;

        StopAllCoroutines();
        StartCoroutine(TypeWord(teachSystem_Content[index].content[contentIndex]));
    }
    
    private void ToNextTimerCooling()
    {
        canNext = false;
        Task.Delay(250).ContinueWith(t => canNext = true);
    }
    IEnumerator TypeWord(string Content)
    {
        content.text = "";
        foreach (char letter in Content.ToCharArray())
        {
            content.text += letter;
            yield return null;
        }
    }
    private void setTeachBar(bool active)
    {
        teachBar.SetActive(active);
    }
    private async void setScriptActiv(bool active)
    {
        await Task.Delay(250);
        for(int i=0;i<Script.Length;i++)
        {
            Script[i].SetActive(active);
        }
    }
    private void SetIsTeach(bool active)
    {
        isTeach = active;
    }
    private void OnValidate()
    {
        string debugText = "";
        for(int i=0;i<teachSystem_Content.Length;i++)
        {
            debugText += teachSystem_Content[i].title;

            for(int i2 = 0; i2 < teachSystem_Content[i].content.Length;i2++)
            {
                debugText += teachSystem_Content[i].content[i2];
            }
        }
        this.debugText = debugText;
    }
    private void OnVideoPrepared(VideoPlayer source)
    {
        // Video prepared logic
        source.Play(); // 使用 source.Play() 而不是 teachVideo.videoPlayer.Play()
    }
}
