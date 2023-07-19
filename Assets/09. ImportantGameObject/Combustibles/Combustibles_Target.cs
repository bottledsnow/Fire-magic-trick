using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustibles_Target : MonoBehaviour
{
    private CombustiblesObj combustiblesObj;
    [SerializeField] private Collider AutoCollider;
    private void Start()
    {
        combustiblesObj = GetComponent<CombustiblesObj>();
    }

    private void Update()
    {
        OpenAutoTargetCollider();
        CloseAutoTargetCollider();
    }

    private void OpenAutoTargetCollider()
    {
        if(combustiblesObj.burning)
        {
            AutoCollider.enabled = true;
        }
    }
    private void CloseAutoTargetCollider()
    {
        if(!combustiblesObj.burning)
        {
            AutoCollider.enabled = false;
        }
    }
}
