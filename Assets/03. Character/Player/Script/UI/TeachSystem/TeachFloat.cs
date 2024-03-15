using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TeachFloat_Small
{
    public string word;
    public Image ButtonUI;
}
[System.Serializable]
public class TeachFloat_Big
{
    public string Title;
    public string content;
}
public class TeachFloat : MonoBehaviour
{
    [Header("Content")]
    public TeachFloat_Small[] Smalls;
    public TeachFloat_Big[] Bigs;

    //Script
    private ControllerInput input;
    
    //Variable
    [SerializeField] private GameObject TeachBarFloat;
    [SerializeField] private Image Small;
    [SerializeField] private Image Big;
    
    private void Awake()
    {
        TeachBarFloat.SetActive(false);
    }
    private void Start()
    {
        input = GameManager.singleton.Player.GetComponent<ControllerInput>();
    }
    public void Open_Small(int index)
    {

    }
    public void Close_Big(int index)
    {

    }
    private void ChangeSmall()
    {

    }
}
