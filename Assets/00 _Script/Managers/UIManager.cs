using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private InputSystemUIInputModule inputSystemUIInputModule;
    [field: SerializeField] public HUDUI HudUI { get;private set; }
    [SerializeField] private PauseUI pauseUI;
    [SerializeField] private DeathUI deathUI;

    [SerializeField] private TeachUI teachUI;
    public event Action OnTeachEnd;

    private bool canPause = true;
    [SerializeField] private DialogueUI dialogueUI;
    public event Action OnDisplayNextSentence;
    public event Action OnDialogueEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //TODO: Lock pause menu when in dialogue and in timeline
        canPause = true;
    }

    private void OnEnable()
    {
        pauseUI.gameObject.SetActive(false);
        deathUI.gameObject.SetActive(false);
        HudUI.gameObject.SetActive(true);
        teachUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInputHandler.Instance.ESCInput)
        {
            PlayerInputHandler.Instance.UseESCInput();

            if (!pauseUI.gameObject.activeInHierarchy &&
                !teachUI.gameObject.activeInHierarchy &&
                !dialogueUI.gameObject.activeInHierarchy)
            {
                pauseUI.Activate();
            }
            else if (teachUI.gameObject.activeInHierarchy)
            {
                teachUI.CloseTeach();
            }
            else
            {
                pauseUI.Deactivate();
            }
        }
    }

    public void SetCanPause(bool value)
    {
        canPause = value;
    }


    public void ActivateDeathUI()
    {
        deathUI.Activate();
    }

    public void DeactivateDeathUI()
    {
        deathUI.Deactivate();
    }

    public void ActivatePauseMenu()
    {
        pauseUI.Activate();
    }

    public void DeactivatePauseMenu()
    {
        pauseUI.Deactivate();
    }

    public void ActivateTeachUI(int index)
    {
        teachUI.OpenTeach(index);
        DeactivateHUD();
    }

    public void OnDecativateTeachUI()
    {
        OnTeachEnd?.Invoke();
        ActivateHUD();
    }

    #region HUD

    public void SetCrossRed()
    {
        HudUI.SetCrossRed();
    }

    public void SetCrossWhite()
    {
        HudUI.SetCrossWhite();
    }

    public void CrosshairShooting()
    {
        HudUI.CrosshairShooting();
    }

    public void HitEnemyEffect()
    {
        HudUI.HitEnemyEffect();
    }

    public void ActivateHUD()
    {
        HudUI.Activate();
    }

    public void DeactivateHUD()
    {
        HudUI.Deactivate();
    }

    public void OpenTeachFloat(TeachFloat.types type)
    {
        HudUI.OpenTeachFloat(type);
    }

    public void CloseTeachFloat(TeachFloat.types type)
    {
        HudUI.CloseTeachFloat(type);
    }
    #endregion

    #region Dialogue
    public void StartDialogue(SO_Dialogue dialogue)
    {
        dialogueUI.StartDialogue(dialogue);
    }
    public void StartDialogue(SO_Dialogue dialogue, float time)
    {
        dialogueUI.StartDialogue(dialogue, time);
    }

    public void DialogueEnd()
    {
        OnDialogueEnd?.Invoke();
    }

    public void DisplayNextSentence()
    {
        OnDisplayNextSentence?.Invoke();
    }
    #endregion

    public bool IsDeathUIOpenFinished()
    {
        return deathUI.FinishedOpenAnim;
    }
}
