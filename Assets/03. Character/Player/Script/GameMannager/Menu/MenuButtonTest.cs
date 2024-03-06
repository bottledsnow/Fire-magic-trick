using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonTest : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        this.button = GetComponent<Button>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            button.Select();
        }
    }
    public void OnClick()
    {
        Debug.Log("Onclick");
    }
}
