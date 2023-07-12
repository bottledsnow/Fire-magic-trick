using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;
using Cinemachine;
using MoreMountains.Feedbacks;

public class Fire_Teleport : MonoBehaviour
{
    public int TeleportDamage;
    [SerializeField] MMFeedbacks TeleportFeedback;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float TeleportCost;
    [SerializeField] private float TeleporCameraDamping;
    [SerializeField] private int OutControll_ms;
    private EnergySystem energySystem;
    private ControllerInput _input;
    private ThirdPersonController _PlayerControl;
    private FireCheck_Easy fireCheck;
    private Vector3 oldCameraDamping;
    private bool canTeleport;

    private void Update()
    {
        ignit();
    }
    private void Start()
    {
        energySystem = GetComponent<EnergySystem>();
        oldCameraDamping = playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping;
        _input = GameManager.singleton._input;
        _PlayerControl = GetComponent<ThirdPersonController>();
        fireCheck = Camera.main.GetComponent<FireCheck_Easy>();
    }
    private void ignit()
    {
        if(_input.RB )
        {
            if(fireCheck.isChooseFirePoint && fireCheck.FirePoint != null)
            {
                energySystem.ConsumeFireEnergy(TeleportCost, out canTeleport);
                if(canTeleport)
                {
                    fireCheck.isChooseFirePoint = false;
                    TeleportFeedback.PlayFeedbacks();
                    FireTeleprot();
                    fireCheck.AbsorbFire();
                    fireCheck.DestroyFirePoint();
                }else
                {
                    Debug.Log("Not enough Fire Energy");
                }
            }
        }
    }
    private async void FireTeleprot()
    {
        SetTeleportCameraDamping();
        _PlayerControl.enabled = false;
        transform.position = fireCheck.FirePoint.position;
        await Task.Delay(OutControll_ms);
        _PlayerControl.enabled = true;
        revertTeleportCameraDamping();
    }
    private void SetTeleportCameraDamping()
    {
        Vector3 newCameraDamping = new Vector3(TeleporCameraDamping, TeleporCameraDamping, TeleporCameraDamping);
        playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = newCameraDamping;
    }
    private void revertTeleportCameraDamping()
    {
        playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = oldCameraDamping;
    }
    
}
