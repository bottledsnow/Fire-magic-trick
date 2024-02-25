using MoreMountains.Feedbacks;
using System.Threading.Tasks;
using UnityEngine;

public class TriggerArea_DialogueTrigger : MonoBehaviour
{
    [Header("Additionals")]
    public MMF_Player NeedFeedbacks;

    [Header("Auto")]
    public bool useAuto;
    public float OnceAutoTime;
    //Variables
    public bool triggerOnce;
    private bool canTrigger = true;
    private bool isReadyDialogue;
    
    [Header("Input To TMP")]
    [SerializeField][TextArea(5, 10)] public string debugText;

    //Script
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    private PlayerState playerState;

    private void Start()
    {
        dialogueManager = GameManager.singleton.UISystem.GetComponent<DialogueManager>();
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        canTrigger = true;
    }
    private void Update()
    {
        if(isReadyDialogue)
        {
            if (playerState.isGround)
            {
                EventTrigger();
                SetIsReadyDialogue(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isReadyDialogue)
            {
                SetIsReadyDialogue(true);
            }
        }
    }
    public async void EventTrigger()
    {
        await Task.Delay(250);
        if (canTrigger)
        {
            if (useAuto)
            {
                dialogueManager.StartDialogue(dialogue, OnceAutoTime);
                dialogueManager.OnDialogueEnd += DialogueEnd;
            }
            else
            {
                dialogueManager.StartDialogue(dialogue);
                dialogueManager.OnDialogueEnd += DialogueEnd;
            }

            if (triggerOnce)
            {
                triggerOnce = false;
                canTrigger = false;
            }
        }
    }
    private void DialogueEnd()
    {
        if(NeedFeedbacks != null)
        {
            NeedFeedbacks.PlayFeedbacks();
        }
    }
    private void SetIsReadyDialogue(bool isReady)
    {
        isReadyDialogue = isReady;
    }
    private void OnValidate()
    {
        string debugText = "";

        for (int i = 0; i < dialogue.contents.Length; i++)
        {
            debugText += dialogue.contents[i].name;
            debugText += dialogue.contents[i].sentences;
        }
        this.debugText = debugText;
    }
}
