using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class ImpactReceiver : MonoBehaviour
{
    [SerializeField] private float drag;

    private Vector3 impact = Vector3.zero;
    private CharacterController characterController;

    [Header("new Impact")]
    [SerializeField] private float impactTime;
    [SerializeField] private AnimationCurve smoothCurve;

    [Header("Test")]
    public float yMuti;

    //script
    private PlayerState playerState;

    //variable
    private Vector3 ForceImpact;
    private Vector3 deltaForce;
    private float deltaTime;
    private bool isImpact;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {
        if (impact.magnitude > 0.2)
        {
            characterController.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, drag * Time.deltaTime);

        ImpactSystem();
    }
    private void ImpactSystem()
    {
        if (isImpact)
        {
            deltaTime += Time.deltaTime / impactTime;

            Impact();
        }
    }
    private void Impact()
    {
        deltaForce = smoothCurve.Evaluate(deltaTime) * ForceImpact;
        Vector3 xz = new Vector3(deltaForce.x, 0, deltaForce.z);
        Vector3 y = new Vector3(0, deltaForce.y, 0);

        characterController.Move(xz * Time.deltaTime);
        playerState.AddVerticalVelocity(y.y* yMuti * Time.deltaTime);
    }
    public void AddImpact(Vector3 force)
    {
        impact += force;
    }
    public async void ToImpact(Vector3 force)
    {
        SetIsImpact(true);
        ForceImpact = force;
        await Task.Delay((int)(impactTime * 1000));
        SetIsImpact(false);
        ForceImpact = Vector3.zero;
    }
    public async void ToImpact(Vector3 force,float impactTime)
    {
        SetIsImpact(true);
        ForceImpact = force;
        await Task.Delay((int)(impactTime * 1000));
        playerState.SetVerticalVelocity(0);
        SetIsImpact(false);
        ForceImpact = Vector3.zero;
    }
    private void SetIsImpact(bool value)
    {
        isImpact = value;

        if (isImpact)
        {
            deltaTime = 0;
        }
    }
}
