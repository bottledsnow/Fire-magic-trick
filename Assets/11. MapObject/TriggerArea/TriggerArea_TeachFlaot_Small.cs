using UnityEngine;

public class TriggerArea_TeachFlaot_Small : MonoBehaviour
{
    //Script
    [SerializeField] private TeachFloat teachFloat;

    //variable
    [Header("Button Number")]
    public bool once;
    [SerializeField] private int index;
    private bool isTrigger;

    private void Start()
    {
        teachFloat = GameManager.singleton.Player.GetComponent<TeachFloat>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isTrigger)
            {
                if (once) SetIsTrigger(true);
                teachFloat.Open_Small(index);
            }
        }
    }
    private void SetIsTrigger(bool active)
    {
        isTrigger = active;
    }
}
