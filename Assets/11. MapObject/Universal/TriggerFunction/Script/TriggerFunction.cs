using UnityEngine;
using UnityEngine.Events;

public class TriggerFunction : MonoBehaviour
{
    [SerializeField] UnityEvent OnTriggerEvent;
    [SerializeField] private bool OnlyOnce = true;
    [Header("OnStartTrigger")]
    [SerializeField] private bool OnStartTrigger = false;
    [SerializeField] UnityEvent OnStartTriggerEvent;


    private void Start()
    {
        if(OnStartTrigger)
        {
            OnStartTriggerEvent.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEvent.Invoke();
            if (OnlyOnce)
            {
                Destroy(gameObject);
            }
        }
    }

}
