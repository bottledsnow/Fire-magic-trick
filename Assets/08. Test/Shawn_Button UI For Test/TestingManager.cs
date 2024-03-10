using StarterAssets;
using UnityEngine;

public class TestingManager : MonoBehaviour
{

    //Script
    [SerializeField] private GameObject InputEvent;
    private StarterAssetsInputs assetsInput;
    //Variable
    [SerializeField] private bool useTest;
    private void Awake()
    {
        if(!useTest)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        InputEvent.gameObject.transform.parent = this.gameObject.transform;
        assetsInput = GameManager.singleton.Player.GetComponent<StarterAssetsInputs>();

        assetsInput.cursorLocked = false;
        assetsInput.cursorInputForLook = false;
    }
}
