using UnityEngine;

public class DestroyThisObj : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(this,lifeTime);
    }
}
