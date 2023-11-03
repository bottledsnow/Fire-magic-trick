using MoreMountains.Feedbacks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SenceManager : MonoBehaviour
{
    private ProgressSystem _progressSystem;
    private BrokeGlass BigGlass;
    private GlassRoad GlassRoadScript;
    private Transform[] EnemyFly = new Transform[2];
    private Transform glassPlatform;
    private Transform glassWall;
    private Transform groundGlass;
    [Header("StartArea")]
    public bool PassTeach;
    [SerializeField] private Collider ColliderLand;
    [Header("Position")]
    [SerializeField] private Transform GlassPosition;
    [SerializeField] private Transform EnemyPosition;
    [SerializeField] private Transform GlassRoadPosition;
    [SerializeField] private Transform[] EnemyFlyPositions;
    [SerializeField] private Transform GlassPlatformPosition;
    [SerializeField] private Transform GlassWallPosition;
    [SerializeField] private Transform GroundGlassPosition;

    [Header("Preferb")]
    [SerializeField] private Transform Glass;
    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform GlassRoad;
    [SerializeField] private Transform GlassPlatform;
    [SerializeField] private Transform GlassWall;
    [SerializeField] private Transform GroundGlass;

    [Header("Feedback")]
    [SerializeField] private MMF_Player GlassBroke;

    [Header("TeachTitle")]
    [SerializeField]  private GameObject TeachTitle;
    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();

        if(PassTeach)
        {
            ExitTeachArea();
        }
        else
        {
            InitiaTeachArea();
        }
    }
    private void InitiaTeachArea()
    {
        _progressSystem.OnPlayerDeath += StartAreaRebirth;
        _progressSystem.OnPlayerDeath += RebirthEnemy;
        _progressSystem.OnPlayerDeath += DestroyGlassRoad;
        _progressSystem.OnPlayerDeath += RebirthGlassRoad;
        _progressSystem.OnPlayerDeath += DestroyEnemyFly;
        _progressSystem.OnPlayerDeath += RebirthEnemyFly;
        _progressSystem.OnPlayerDeath += DestoryGlassPlatform;
        _progressSystem.OnPlayerDeath += RebirthGlassPlatform;
        _progressSystem.OnPlayerDeath += DestoryGlassWall;
        _progressSystem.OnPlayerDeath += RebirthGlassWall;
        _progressSystem.OnPlayerDeath += DestoryGroundGlass;
        _progressSystem.OnPlayerDeath += RebirthGroundGlass;

        RebirthGlass();
        RebirthEnemy();
        RebirthGlassRoad();
        RebirthEnemyFly();
        RebirthGlassPlatform();
        RebirthGlassWall();
        RebirthGroundGlass();
    }
    
    
    public void GlassBroken()
    {
        ColliderLand.enabled = false;
        GlassBroke.PlayFeedbacks();
        BigGlass.Broke();
        StartGlassRoadBroken();
        Destroy(BigGlass.gameObject, 3f);
    }
    private void RebirthGlass()
    {
        Transform glass = Instantiate(Glass, GlassPosition.transform.position, GlassPosition.transform.rotation);
        BigGlass = glass.GetComponent<BrokeGlass>();
        ColliderLand.enabled = true;
    }
    private void StartGlassRoadBroken()
    {
        GlassRoadScript.GlassBrokenStart();
    }
    public void ExitTeachArea()
    {
        _progressSystem.OnPlayerDeath -= StartAreaRebirth;
        _progressSystem.OnPlayerDeath -= RebirthEnemy;
        _progressSystem.OnPlayerDeath -= DestroyGlassRoad;
        _progressSystem.OnPlayerDeath -= RebirthGlassRoad;
        _progressSystem.OnPlayerDeath -= DestroyEnemyFly;
        _progressSystem.OnPlayerDeath -= RebirthEnemyFly;
        _progressSystem.OnPlayerDeath -= DestoryGlassPlatform;
        _progressSystem.OnPlayerDeath -= RebirthGlassPlatform;
        _progressSystem.OnPlayerDeath -= DestoryGlassWall;
        _progressSystem.OnPlayerDeath -= RebirthGlassWall;
        _progressSystem.OnPlayerDeath -= DestoryGroundGlass;
        _progressSystem.OnPlayerDeath -= RebirthGroundGlass;

        TeachTitle.SetActive(false);    
    }
    #region Rebirth
    public void RebirthEnemy()
    {
        Instantiate(Enemy, EnemyPosition.transform.position, EnemyPosition.transform.rotation);
    }
    public void RebirthGlassRoad()
    {
        Transform tran = Instantiate(GlassRoad, GlassRoadPosition.transform.position, GlassRoadPosition.transform.rotation);
        GlassRoadScript = tran.GetComponent<GlassRoad>();
    }
    public void RebirthEnemyFly()
    {
        EnemyFly[0] = Instantiate(Enemy, EnemyFlyPositions[0].transform.position, EnemyFlyPositions[0].transform.rotation);
        EnemyFly[1] = Instantiate(Enemy, EnemyFlyPositions[1].transform.position, EnemyFlyPositions[1].transform.rotation);
    }
    public void RebirthGlassPlatform()
    {
        glassPlatform = Instantiate(GlassPlatform, GlassPlatformPosition.transform.position, GlassPlatformPosition.transform.rotation);
    }
    public void RebirthGlassWall()
    {
        glassWall = Instantiate(GlassWall, GlassWallPosition.transform.position, GlassWallPosition.transform.rotation);
    }
    public void RebirthGroundGlass()
    {
        groundGlass = Instantiate(GroundGlass, GroundGlassPosition.transform.position, GroundGlassPosition.transform.rotation);
    }
    public void DestroyEnemyFly()
    {
        Destroy(EnemyFly[0].gameObject);
        Destroy(EnemyFly[1].gameObject);
    }
    public void DestroyGlassRoad()
    {
        GlassRoadScript.gameObject.SetActive(false);
    }
    public void DestoryGlassPlatform()
    {
        Destroy(glassPlatform.gameObject);
    }
    public void DestoryGlassWall()
    {
        Destroy(glassWall.gameObject);
    }
    public void DestoryGroundGlass()
    {
        Destroy(groundGlass.gameObject);
    }
    #endregion
    #region Area Progress
    private void StartAreaRebirth()
    {
        RebirthGlass();
    }
    #endregion
}
