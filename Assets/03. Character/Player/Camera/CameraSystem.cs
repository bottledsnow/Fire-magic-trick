using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera TeleportCamera;
    [SerializeField] private float Distance;
    [SerializeField] private float cameraHight;

    private Transform playerPosition;
    private Transform cameraTarget;
    private FireCheck fireCheck;
    private void Start()
    {
        playerPosition = GameManager.singleton.Player;
        fireCheck = GameManager.singleton._fireCheck;
    }
    public async void ToTeleportCamera()
    {
        ToTeleportCamera_Start();
        await Task.Delay(700);
        ToTeleportCamera_End();
    }
    public void ToTeleportCamera_Start()
    {
        GetTarget();
        MoveCameraToTarget();
        ClosePlayerCamera();
        OpenTeleportCamera();
    }
    public void ToTeleportCamera_End()
    {
        CloseTeleportCamera();
        OpenPlayerCamera();
    }
    private void GetTarget()
    {
        cameraTarget = fireCheck.FirePoint;
        TeleportCamera.LookAt = cameraTarget;
    }
    private void MoveCameraToTarget()
    {
        Vector3 direction = (playerPosition.position - cameraTarget.transform.position).normalized;
        Vector3 CameraPositoin = cameraTarget.position + direction * Distance;
        Vector3 hight = new Vector3(0, cameraHight, 0);
        TeleportCamera.transform.position = CameraPositoin + hight;
    }
    private void ClosePlayerCamera()
    {
        playerCamera.gameObject.SetActive(false);
    }
    private void OpenTeleportCamera()
    {
        TeleportCamera.gameObject.SetActive(true);
    }
    private void CloseTeleportCamera()
    {
        TeleportCamera.gameObject.SetActive(false);
    }
    private void OpenPlayerCamera()
    {
        playerCamera.gameObject.SetActive(true);
    }
}
