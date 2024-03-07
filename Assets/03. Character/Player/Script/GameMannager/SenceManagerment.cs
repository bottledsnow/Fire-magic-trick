using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceManagerment : MonoBehaviour
{
    private StarterAssetsInputs input;

    private void Start()
    {
        input= GameManager.singleton.Player.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ReStartGame();
        }
    }
    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
        input.cursorInputForLook = false;
        input.cursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
