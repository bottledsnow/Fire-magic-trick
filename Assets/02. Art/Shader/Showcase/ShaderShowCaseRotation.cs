using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderShowCaseRotation : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        
    }

    void Update()
    {
        float rotationAmount = speed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount);
    }
}
