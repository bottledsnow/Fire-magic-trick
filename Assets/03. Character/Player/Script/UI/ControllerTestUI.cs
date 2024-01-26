using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTestUI : MonoBehaviour
{
    [SerializeField] private GameObject TestControllerUI;

    private void Start()
    {
        TestControllerUI.SetActive(true) ;
    }
}
