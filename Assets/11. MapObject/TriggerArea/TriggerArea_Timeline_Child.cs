using UnityEngine;

public class TriggerArea_Timeline_Child : MonoBehaviour
{
    //script
    private TriggerArea_Timeline timeline;

    private void Awake()
    {
        timeline = GetComponentInParent<TriggerArea_Timeline>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeline.readyPlay();
        }
    }
}
