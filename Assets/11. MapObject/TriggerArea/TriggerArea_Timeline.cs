using UnityEngine;
using UnityEngine.Playables;

public class TriggerArea_Timeline : MonoBehaviour
{
    private PlayableDirector timelinePlayable;

    private void Start()
    {
        timelinePlayable = GetComponent<PlayableDirector>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timelinePlayable.Play();
        }
    }
}
