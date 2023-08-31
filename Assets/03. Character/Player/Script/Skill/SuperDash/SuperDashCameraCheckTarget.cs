using UnityEngine;

public class SuperDashCameraCheckTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
    }
}
