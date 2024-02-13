using UnityEngine;

public class TriggerArea_DialogueTrigger : MonoBehaviour
{
    //Script
    private DialogueManager dialogueManager;
    public Dialogue dialogue;

    //Variables
    public bool triggerOnce;
    private bool canTrigger = true;

    private void Start()
    {
        dialogueManager = GameManager.singleton.UISystem.GetComponent<DialogueManager>();

        canTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnce)
        {
            triggerOnce = false;
            canTrigger = false;
        }

        if (other.CompareTag("Player"))
        {
            if (canTrigger)
            {
                TriggerDialogue();
            }
        }
    }
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
    
    
}
