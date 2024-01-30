using UnityEngine;
using TMPro;
using System.Threading.Tasks;

[System.Serializable]
public class TeachSystem_content
{
    public string title;
    [TextArea(3,10)]public string content;
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
    private PauseSystem pauseSystem;

    //variable
    private int index = 0;

    private void Start()
    {
        pauseSystem = GameManager.singleton.GetComponent<PauseSystem>();
        input = GameManager.singleton._input;

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
            if(input.ButtonA)
            {
                //Continue
            }
            if(input.ButtonB)
            {
                pauseSystem.StopPause();
                CloseTeach();
            }
        }
    }
    public void OpenTeach(int index)
    {
        this.index = index;
        title.text = teachSystem_Content[index].title;
        content.text = teachSystem_Content[index].content;
        setTeachBar(true);
        setScriptActiv(false);
        pauseSystem.Pause();
    }
    public void CloseTeach()
    {
        setTeachBar(false);
        setScriptActiv(true);
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
    private void OnValidate()
    {
        string debugText = "";
        for(int i=0;i<teachSystem_Content.Length;i++)
        {
            debugText += teachSystem_Content[i].title;
            debugText += teachSystem_Content[i].content;
        }
        this.debugText = debugText;
    }
}
