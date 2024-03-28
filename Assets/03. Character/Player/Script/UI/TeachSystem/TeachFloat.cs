using UnityEngine;
using UnityEngine.UI;

public class TeachFloat : MonoBehaviour
{
    [SerializeField] private GameObject UI_SuperJump;
    [SerializeField] private GameObject DashState;

    private void Start()
    {
        UI_SuperJump.SetActive(false);
        DashState.SetActive(false);
    }
    public enum types
    {
        SuperJump,
        DashState,
        FireSkill,
        WindSkill
    }
    public void Open(types type)
    {
        GameObject obj = null;

        switch (type)
        {
            case types.SuperJump:
                obj = UI_SuperJump;
                break;
            case types.DashState:
                obj = DashState;
                break;
        }

        obj.SetActive(true);
    }
    public void Close(types type)
    {
        GameObject obj = null;

        switch (type)
        {
            case types.SuperJump:
                obj = UI_SuperJump;
                break;
            case types.DashState:
                obj = DashState;
                break;
        }

        obj.SetActive(false);
    }
}
