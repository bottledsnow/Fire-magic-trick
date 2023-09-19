using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class PrototypeN3 : MonoBehaviour
{
    [SerializeField] private MMF_Player Feedbacks;
    [SerializeField] private MMF_Player Feedbacks2;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    private ControllerInput _input;
    private bool trigger = false;
    public bool isFalling = false;
    private void Start()    
    {
        _input = GameManager.singleton._input;
    }
    private void Update()
    {
        ToRecoverTimeScale();
    }
    private void ToRecoverTimeScale()
    {
        if(_input.ButtonA && !trigger && isFalling)
        {
            Feedbacks.StopFeedbacks();
            Feedbacks2.PlayFeedbacks();
            trigger = true;
            isFalling = false;
        }
    }
}
