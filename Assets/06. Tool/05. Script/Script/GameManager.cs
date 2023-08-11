using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager singleton = null;
    public ControllerInput _input;
    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }
}
