using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactReceiver : MonoBehaviour
{
    [SerializeField] private float drag;

    private Vector3 impact = Vector3.zero;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (impact.magnitude > 0.2)
        {
            characterController.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, drag * Time.deltaTime);
    }

    public void AddImpact(Vector3 force)
    {
        impact += force;
    }
}
