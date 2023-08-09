using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControll : MonoBehaviour
{
    public GameObject Camera;
    public GameObject AimCamera;
    public bool Aim;
    public void Camera_normal()
    {
        Camera.SetActive(true);
        AimCamera.SetActive(false);
    }
    public void Camera_Aim()
    {
        Camera.SetActive(false);
        AimCamera.SetActive(true);
    }
}
