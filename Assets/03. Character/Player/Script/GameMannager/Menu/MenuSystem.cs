using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject MenuUI;
    private GameObject MainCamera;
    private GameManager gameManager;
    private GameObject Player;

    private void Start()
    {
        gameManager = GameManager.singleton;
        Player = GameManager.singleton.Player.gameObject;
        MainCamera = Camera.main.gameObject;
        Initialization();
    }
    #region For test
    #endregion
    private void Initialization()
    {
        NullFatherObj(gameManager.gameObject);
        NullFatherObj(MainCamera.gameObject);
        NullFatherObj(MenuUI);
    }
    private void NullFatherObj(GameObject obj)
    {
        obj.gameObject.transform.parent = null;
    }
    #region  playMode
    #endregion
    private void SetPlayMode(bool active)
    {
        Player.SetActive(active);

    }
    private void SetMenuInterface(bool active)
    {
        MenuUI.gameObject.SetActive(active);
    }
}
