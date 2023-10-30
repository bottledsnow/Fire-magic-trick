using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SenceManager : MonoBehaviour
{
    private ProgressSystem _progressSystem;
    private BrokeGlass BigGlass;
    [SerializeField] private Collider ColliderLand;
    [Header("Position")]
    [SerializeField] private Transform GlassPosition;
    [Header("Preferb")]
    [SerializeField] private Transform Glass;
    [Header("Feedback")]
    [SerializeField] private MMF_Player GlassBroke;
    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();

        _progressSystem.OnPlayerDeath += StartAreaRebirth;

        CreateGlass();
    }
    
    private void Update()
    {
        Test();
    }
    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateGlass();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GlassBroken();
        }
    }
    public void GlassBroken()
    {
        ColliderLand.enabled = false;
        GlassBroke.PlayFeedbacks();
        BigGlass.Broke();
        Destroy(BigGlass.gameObject, 3f);
    }
    private void CreateGlass()
    {
        Transform glass = Instantiate(Glass, GlassPosition.transform.position, GlassPosition.transform.rotation);
        BigGlass = glass.GetComponent<BrokeGlass>();
        ColliderLand.enabled = true;
    }
    #region Area Progress
    private void StartAreaRebirth()
    {
        CreateGlass();
    }
    #endregion
}
