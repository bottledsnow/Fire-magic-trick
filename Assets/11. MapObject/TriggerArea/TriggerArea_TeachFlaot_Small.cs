using UnityEngine;

public class TriggerArea_TeachFlaot_Small : MonoBehaviour
{
    //Script
    private ControllerInput input;

    //variable
    [Header("Button Number")]
    protected bool once;

    private void Start()
    {
        input = GameManager.singleton.Player.GetComponent<ControllerInput>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
    protected virtual void Open()
    {

    }
    protected virtual void Close()
    {

    }

}
