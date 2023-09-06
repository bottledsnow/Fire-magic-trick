using UnityEngine;

public class DestoryHitObj : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1.0f);
    }
}
