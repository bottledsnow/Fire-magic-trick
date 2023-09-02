using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float Distance;
    [SerializeField] private float cameraHight;

    private Transform playerPosition;
    private Transform cameraTarget;
    private void Start()
    {
        playerPosition = GameManager.singleton.Player;
    }
    public async void ToTeleportCamera()
    {
        ToTeleportCamera_Start();
        await Task.Delay(700);
    }
    public void ToTeleportCamera_Start()
    {
        GetTarget();
        MoveCameraToTarget();
        ClosePlayerCamera();
    }

    private void GetTarget()
    {
        
    }
    private void MoveCameraToTarget()
    {
        Vector3 direction = (playerPosition.position - cameraTarget.transform.position).normalized;
        Vector3 CameraPositoin = cameraTarget.position + direction * Distance;
        Vector3 hight = new Vector3(0, cameraHight, 0);
    }
    private void ClosePlayerCamera()
    {
        playerCamera.gameObject.SetActive(false);
    }
    private void OpenPlayerCamera()
    {
        playerCamera.gameObject.SetActive(true);
    }
}
