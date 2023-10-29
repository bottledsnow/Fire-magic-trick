using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Camera_Normal;
    [SerializeField] private CinemachineVirtualCamera Camera_Run;
    [SerializeField] private CinemachineVirtualCamera Camera_Aim;
    [SerializeField] private CinemachineVirtualCamera Camera_Death;

    public void useCamera_normal()
    {

    }
    public void useCamera_Run()
    {

    }
    public void useCamera_Aim()
    {

    }
    public void useCamera_Death()
    {
        Camera_Death.gameObject.SetActive(true);
        Camera_Aim.gameObject.SetActive(false);
        Camera_Run.gameObject.SetActive(false);
        Camera_Normal.gameObject.SetActive(false);
    }
}
