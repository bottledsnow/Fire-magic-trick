using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class CardFall : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private MMF_Player Feedbacks;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        FallDown();
    }
    
    private void FallDown()
    {
        rb.velocity = Vector3.down * fallSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
