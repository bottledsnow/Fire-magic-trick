using Unity.VisualScripting;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] private GameObject Preferb;
    [SerializeField] private Transform spawnPoint;

    private MovePlatform movePlatform;
    private GameObject clone;

    private bool isReturn;
    private bool Trigger;

    private void Start()
    {
        movePlatform = GetComponent<MovePlatform>();
    }
    private void Update()
    {
        CheckisReturn();
        TransportSystem();
    }
    private void TransportSystem()
    {
        OnStart();
        OnReturn();
    }
    private void CheckisReturn()
    {
        isReturn = movePlatform.isReturn;
    }
    private void OnStart()
    {
        if(!Trigger && !isReturn)
        {
            SetTrigger(true);
            ToShowPlatform();
            spawnPreferb();
        }
    }
    private void OnReturn()
    {
        if(Trigger && isReturn)
        {
            SetTrigger(false);
            ToHidePlatform();
            ClearPreferb();
        }
    }
    private void ToHidePlatform()
    {
        movePlatform.SetActivePlatform(false);
    }
    private void ToShowPlatform()
    {
        movePlatform.SetActivePlatform(true);
    }
    private void ClearPreferb()
    {
        if(clone != null)
        {
            Destroy(clone);
        }
    }
    private void spawnPreferb()
    {
        clone = Instantiate(Preferb, spawnPoint.position, Quaternion.identity);
        clone.transform.parent = spawnPoint;
    }
    private void SetTrigger(bool active)
    {
        Trigger = active;
    }
}
