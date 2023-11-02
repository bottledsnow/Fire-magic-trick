using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMannager : MonoBehaviour
{
    private ControllerInput _input;
    [SerializeField] private GameObject thisCanva;
    [SerializeField] private GameObject Player;
    private bool trigger;
    private void Start()
    {
        _input = GetComponent<ControllerInput>();
    }
    
    public void LoadStartSence()
    {
        SceneManager.LoadScene(1);
        //thisCanva.SetActive(false);
        //Player.SetActive(true);
    }
    public void LoadLevelChose()
    {

    }
    public void LoadCreator()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
