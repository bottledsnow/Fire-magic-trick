using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost : MonoBehaviour
{
    [SerializeField] private Transform ghostPosition;
    [SerializeField] private GameObject player;
    private Rigidbody rb;
    private Vector3 playerforward;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.position = ghostPosition.position;
        Quaternion playerQ = player.transform.rotation;
        transform.rotation = Quaternion.Euler(-90, 0, playerQ.z);
        playerforward = player.gameObject.transform.forward;
    }
    private void FixedUpdate()
    {
        rb.velocity = playerforward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position,this.gameObject.transform.forward);
    }
}
