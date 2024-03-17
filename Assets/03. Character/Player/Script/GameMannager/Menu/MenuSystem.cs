using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;

public class MenuSystem : MonoBehaviour
{
    public bool isStartGame;
    public bool useMenu;
    [Header("Opening")]
    [SerializeField] private GameObject OpeningObj;
    [Header("UI")]
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject Script;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject CameraPakage;
    [Header("Opening")]
    [SerializeField] private PlayableDirector Opening;
    private ThirdPersonController thirdPersonController;
    private EnergySystem energySystem;
    private GameObject MainCamera;
    private GameManager gameManager;
    private GameObject Player;
    private PlayerState state;
    private StarterAssetsInputs starterAssets;
    private void Awake()
    {
        NullFatherObj(CameraPakage);
    }
    private void Start()
    {
        starterAssets = GameManager.singleton.Player.GetComponent<StarterAssetsInputs>();
        gameManager = GameManager.singleton;
        thirdPersonController = GameManager.singleton.Player.GetComponent<ThirdPersonController>();
        energySystem = GameManager.singleton.Player.GetComponent<EnergySystem>();
        MainCamera = Camera.main.gameObject;
        Player = GameManager.singleton.Player.gameObject;
        state = GameManager.singleton.Player.GetComponent<PlayerState>();

        if(useMenu)
        {
            Initialization();
            OpeningObj.SetActive(true);
            Player.transform.parent = OpeningObj.gameObject.transform;
        }
        else
        {
            OpeningObj.SetActive(false);
            Player.transform.parent = null;
        }
    }
    #region For test
    public void Test_SetMenuInterface(bool active)
    {
        SetMenuInterface(active);
    }
    public void CheckMenuUse()
    {
        if(useMenu)
        {
            MenuUI.SetActive(true);
        }
        else
        {
            MenuUI.SetActive(false);
        }
    }
    #endregion
    private void Initialization()
    {
        NullFatherObj(gameManager.gameObject);
        NullFatherObj(MainCamera.gameObject);
        NullFatherObj(MenuUI);
        //NullFatherObj(PauseUI);
        NullFatherObj(UI);
        NullFatherObj(CameraPakage);

        SetPlayMode(false);
        SetMenuInterface(true);

        if(state != null)
        {
            thirdPersonController.useCameraRotate= false;
            state.OutControl();
        }
        starterAssets.cursorLocked = false;
        starterAssets.cursorInputForLook = false;
    }
    private void NullFatherObj(GameObject obj)
    {
        obj.gameObject.transform.parent = null;
    }
    #region  playMode
    #endregion
    public void SetPlayMode(bool active)
    {
        thirdPersonController.enabled = active;
        energySystem.enabled = active;
        Script.SetActive(active);
        UI.SetActive(active);
    }
    private void SetMenuInterface(bool active)
    {
        MenuUI.gameObject.SetActive(active);
    }
    public void SetisStartGame(bool active)
    {
        isStartGame = active;
    }
    #region MenuButton
    public void StartGame()
    {
        SetMenuInterface(false);
        SetPlayMode(true);
        SetisStartGame(true);

        //Opening
        Player.transform.position = new Vector3(0, 0, 0);
        Opening.Play();
        starterAssets.cursorLocked = true;
        starterAssets.cursorInputForLook = true;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void GameSetting()
    {
        
    }
    public void Credit()
    {

    }

    public void LeaveGame()
    {
        Application.Quit();
    }
    #endregion
    private void OnValidate()
    {
        CheckMenuUse();
    }
}
