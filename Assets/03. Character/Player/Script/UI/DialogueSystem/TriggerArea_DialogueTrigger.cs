using MoreMountains.Feedbacks;
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
    
    [Header("Input To TMP")]
    [SerializeField][TextArea(5, 10)] public string debugText;

    //Script
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    private void Start()
    {
        dialogueManager = GameManager.singleton.UISystem.GetComponent<DialogueManager>();
        canTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canTrigger)
            {
                if(useAuto)
                {
                    dialogueManager.StartDialogue(dialogue,OnceAutoTime);
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
    }
    public void EventTrigger()
    {
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
