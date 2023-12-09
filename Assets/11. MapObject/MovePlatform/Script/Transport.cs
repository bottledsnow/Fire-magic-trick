using UnityEngine;

public class Transport : MonoBehaviour
{
    

    [SerializeField] private GameObject Preferb;
    [SerializeField] private Transform spawnPoint;
    [Header("Mode")]
    [SerializeField] private bool isGlass;

    private GlassSystem glass;
    private MovePlatform movePlatform;
    private GameObject clone;

    private bool isSpawn;
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
        if(isGlass)
        {
            glass.QuickSetGlassFalse();
        }else
        if(clone != null)
        {
            Destroy(clone);
        }
    }
    private void spawnPreferb()
    {
        if(isGlass)
        {
            if(!isSpawn)
            {
                Quaternion rotation = spawnPoint.rotation;
                clone = Instantiate(Preferb, spawnPoint.position, rotation);
                clone.transform.parent = spawnPoint;
                glass = clone.GetComponent<GlassSystem>();
                SetIsSpawn(true);
            }else
            {
                glass.GlassRebirth();
            }
        }else
        {
            Quaternion rotation = spawnPoint.rotation;
            clone = Instantiate(Preferb, spawnPoint.position, rotation);
            clone.transform.parent = spawnPoint;
            SetIsSpawn(true);
        }
        
    }
    private void SetTrigger(bool active)
    {
        Trigger = active;
    }
    private void SetIsSpawn(bool active)
    {
        isSpawn = active;
    }
}
