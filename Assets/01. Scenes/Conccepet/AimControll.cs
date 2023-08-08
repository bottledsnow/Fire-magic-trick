using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControll : MonoBehaviour
{
    public GameObject Camera;
    public GameObject AimCamera;
    public bool Aim;

    private void Update()
    {
        Switch();
    }

    private void Switch()
    {
        if(Aim)
        {
            Camera.SetActive(false);
            AimCamera.SetActive(true);
        }else
        {
            Camera.SetActive(true);
            AimCamera.SetActive(false);
        }
    }
}
