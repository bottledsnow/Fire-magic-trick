using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject TestGameObject;
    private void Update()
    {
        cameraDirectionTest();
    }

    private void cameraDirectionTest()
    {
        Vector3 Direction = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        TestGameObject.transform.eulerAngles = Direction;
    }
}
