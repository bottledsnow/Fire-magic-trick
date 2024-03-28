using UnityEngine;

public class TriggerArea_TeachFlaot : MonoBehaviour
{
    [SerializeField] TeachFloat.types types;
    //Script
    private TeachFloat teachFloat;
    private void Start()
    {
        teachFloat = GameManager.singleton.UISystem.GetComponent<TeachFloat>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teachFloat.Open(types);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teachFloat.Close(types);
        }
    }
}
