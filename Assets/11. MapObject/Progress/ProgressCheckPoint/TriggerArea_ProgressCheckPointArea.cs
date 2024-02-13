using UnityEngine;

public class TriggerArea_ProgressCheckPointArea : MonoBehaviour
{
    private ProgressSystem _progressSystem;
    private bool Trigger;

    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _progressSystem.ProgressCheckPoint = transform;
            Trigger = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(!Trigger)
        {
            if (other.CompareTag("Player"))
            {
                _progressSystem.ProgressCheckPoint = transform;
            }
            Trigger = true;
        }
    }
}
