using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    [SerializeField] private Transform ghostPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;
    public Vector3 Ghostforward;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = Ghostforward;
    }
}
