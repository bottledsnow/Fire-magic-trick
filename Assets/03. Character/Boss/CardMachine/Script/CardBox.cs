using UnityEngine;

public class CardBox : MonoBehaviour
{
    [SerializeField] private GameObject OutPoint;
    [SerializeField] private float force;
    [SerializeField] private TriggerArea_DialogueTrigger trigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = OutPoint.transform.position;
            other.GetComponent<ImpactReceiver>().AddImpact(OutPoint.transform.forward * force);
            trigger.EventTrigger();
        }
    }
}
