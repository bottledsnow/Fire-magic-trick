using UnityEngine;

public class Event_GlassBox_2D : MonoBehaviour
{
    public Rigidbody[] rb;
    public Collider[] colliders;

    private void OnValidate()
    {
        rb = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }
    public void Broken()
    {
        for(int i = 0; i < rb.Length; i++)
        {
            rb[i].useGravity = true;
        }
        for(int i = 0;i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
}
