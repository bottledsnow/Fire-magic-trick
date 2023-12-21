using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject Script;
    [SerializeField] private GameObject UI;
    private ThirdPersonController thirdPersonController;
    private EnergySystem energySystem;
    private GameObject MainCamera;
    private GameManager gameManager;
    private GameObject Player;

    private void Start()
    {
        gameManager = GameManager.singleton;
        Player = GameManager.singleton.Player.gameObject;
        thirdPersonController = GameManager.singleton.Player.GetComponent<ThirdPersonController>();
        energySystem = GameManager.singleton.Player.GetComponent<EnergySystem>();
        MainCamera = Camera.main.gameObject;

        Initialization();
    }
    #region For test
    public void Test_SetMenuInterface(bool active)
    {
        SetMenuInterface(active);
    }
    #endregion
    private void Initialization()
    {
        NullFatherObj(gameManager.gameObject);
        NullFatherObj(MainCamera.gameObject);
        NullFatherObj(MenuUI);

        SetPlayMode(false);
        SetMenuInterface(true);
    }
    private void NullFatherObj(GameObject obj)
    {
        obj.gameObject.transform.parent = null;
    }
    #region  playMode
    #endregion
    private void SetPlayMode(bool active)
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
    #region MenuButton
    public void StartGame()
    {
        SetMenuInterface(false);
        SetPlayMode(true);
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
}
