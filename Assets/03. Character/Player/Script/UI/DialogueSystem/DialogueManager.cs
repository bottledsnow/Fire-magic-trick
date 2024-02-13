using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Windows;

public class DialogueManager : MonoBehaviour
{
    //Script
    public TMP_Text nameText;
    public TMP_Text DialogueText;
    private PlayerState playerState;
    private HealthSystem healthSystem;
    private ControllerInput input;
    private Queue<string> sentences;
    [SerializeField] private GameObject UI_dialogue;

    //variables
    private bool isDialogue;
    private bool canNext = true;
    private void Start()
    {
        sentences = new Queue<string>();
        UI_dialogue.SetActive(false);
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        healthSystem = GameManager.singleton.Player.GetComponent<HealthSystem>();
        input = GameManager.singleton.Player.GetComponent<ControllerInput>();

        //value
        canNext = true;
    }
    private void Update()
    {
        if (isDialogue)
        {
            if(input.leftClick)
            {
                if(canNext)
                {
                    DisplayNextSentence();
                }
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        InitiaDialogue();
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        ToNextTimer();
        if (sentences.Count ==0)
        { 
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
    }
    private void ToNextTimer()
    {
        canNext = false;
        Task.Delay(250).ContinueWith(t => canNext = true);
    }
    private void EndDialogue()
    {
        if (UI_dialogue.activeSelf == true)
        {
            UI_dialogue.SetActive(false);
        }
        playerState.TakeControl();
        healthSystem.SetStoryInvincible(false);
        playerState.SetUseCameraRotate(true);
        SetIsDialogue(false);
    }
    private void InitiaDialogue()
    {
        if (UI_dialogue.activeSelf == false)
        {
            UI_dialogue.SetActive(true);
        }
        playerState.OutControl();
        playerState.SetUseCameraRotate(false);
        healthSystem.SetStoryInvincible(true);
        SetIsDialogue(true);
    }
    private void SetIsDialogue(bool active)
    {
        isDialogue = active;
    }
}
