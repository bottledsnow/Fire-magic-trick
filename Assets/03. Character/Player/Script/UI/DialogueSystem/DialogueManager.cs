using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class DialogueManager : MonoBehaviour
{
    //Script
    public Image characterIcon;
    public TMP_Text nameText;
    public TMP_Text DialogueText;
    private PlayerState playerState;
    private HealthSystem healthSystem;
    private ControllerInput input;
    private Queue<Dialogue_Content> contents;
    [SerializeField] private GameObject UI_dialogue;

    //event
    public event MyDelegates.OnHandler OnDialogueEnd;

    //variables
    private bool isDialogue;
    private bool isDialogueAuto;
    private bool canNext = true;
    private void Start()
    {
        contents = new Queue<Dialogue_Content>();
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

        nameText.text = dialogue.contents[0].name;
        characterIcon.sprite = dialogue.contents[0].CharacterIcon;

        contents.Clear();

        foreach (Dialogue_Content content in dialogue.contents)
        {
            contents.Enqueue(content);
        }

        DisplayNextSentence();
    }
    public async void StartDialogue(Dialogue dialogue,float onceAutoTime)
    {
        InitialDialogueAuto();
        SetIsDialogueAuto(true);
        nameText.text = dialogue.contents[0].name;
        characterIcon.sprite = dialogue.contents[0].CharacterIcon;

        contents.Clear();

        foreach (Dialogue_Content content in dialogue.contents)
        {
            contents.Enqueue(content);
        }

        DisplayNextSentence();

        for (int i = 0; i < contents.Count+2; i++)
        {
            await Task.Delay((int)(onceAutoTime*1000));
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        ToNextTimerCooling();

        if(contents.Count == 0)
        {
            OnDialogueEnd?.Invoke();
            OnDialogueEnd = null;
            EndDialogue();
            return;
        }

        Dialogue_Content content = contents.Dequeue();
        nameText.text = content.name;
        characterIcon.sprite = content.CharacterIcon;
        if(content.feedback != null) content.feedback.PlayFeedbacks();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(content.sentences));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
    private void ToNextTimerCooling()
    {
        canNext = false;
        Task.Delay(250).ContinueWith(t => canNext = true);
    }
    private void InitiaDialogue()
    {
        if (UI_dialogue.activeSelf == false)
        {
            UI_dialogue.SetActive(true);
        }
        playerState.OutControl_Dialogue();
        playerState.SetUseCameraRotate(false);
        healthSystem.SetStoryInvincible(true);
        SetIsDialogue(true);
    }
    private void InitialDialogueAuto()
    {
        if (UI_dialogue.activeSelf == false)
        {
            UI_dialogue.SetActive(true);
        }
        DialogueText.text = "";
        SetIsDialogueAuto(true);
    }
    private void EndDialogue()
    {
        if (UI_dialogue.activeSelf == true)
        {
            UI_dialogue.SetActive(false);
        }
        if(isDialogueAuto)
        {
            SetIsDialogueAuto(false);
            return;
        }
        healthSystem.SetStoryInvincible(false);
        playerState.SetUseCameraRotate(true);
        playerState.TakeControl_Dialogue();
        SetIsDialogue(false);
    }
    private void SetIsDialogue(bool active)
    {
        isDialogue = active;
    }
    private void SetIsDialogueAuto(bool active)
    {
        isDialogueAuto = active;
    }
}
