using UnityEngine;
using System.Threading.Tasks;

public class NewGamePlay_Basic_Hover : MonoBehaviour
{
    //Script
    private NewGamePlay_FloatShot floatShot;
    private CharacterController characterController;
    private PlayerState playerState;

    [Header("Setting")]
    [SerializeField] protected AnimationCurve HoverCurve;
    [SerializeField] protected float distance;
    [SerializeField] protected float speed;

    protected float deltaSpeed;
    protected float hoverTime;
    protected float HoverCurveTimer;
    protected bool isHover;

    protected virtual void Start()
    {
        characterController = GameManager.singleton.Player.GetComponent<CharacterController>();
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        floatShot = GameManager.singleton.NewGamePlay.GetComponent<NewGamePlay_FloatShot>();
    }
    protected virtual void Update()
    {
        hoverSystem();
    }
    private void hoverSystem()
    {
        if(isHover)
        {
            HoverCurveTimer += Time.deltaTime / hoverTime ;

            elevate();
        }
    }
    private void elevate()
    {
        deltaSpeed = HoverCurve.Evaluate(HoverCurveTimer)*speed;
        characterController.Move(Vector3.up * deltaSpeed * Time.deltaTime);
    }
    public async void ToHover()
    {
        SetIsHover(true);
        if (!floatShot.isFloat)
        {
            playerState.SetGravityToNormal();
        }
        await Task.Delay((int)(hoverTime * 1000));
        if(!floatShot.isFloat)
        {
            playerState.SetGravityToNormal();
        }
        SetIsHover(false);
    }
    private void SetIsHover(bool value)
    {
        isHover = value;

        if(isHover)
        {
            hoverTime = distance / speed;
            HoverCurveTimer = 0;
            playerState.SetVerticalVelocity(0);
        }
    }
}
