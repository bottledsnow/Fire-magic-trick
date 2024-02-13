using MoreMountains.Feedbacks;
using UnityEngine;

public class TriggerArea_DialogueTrigger : MonoBehaviour
{
    [Header("Input To TMP")]
    [SerializeField][TextArea(5, 10)] public string debugText;

    //Script
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    //Variables
    public bool triggerOnce;
    private bool canTrigger = true;

    [Header("Additionals")]
    public MMF_Player NeedFeedbacks;

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
                TriggerDialogue();
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
        NeedFeedbacks.PlayFeedbacks();
    }
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
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
