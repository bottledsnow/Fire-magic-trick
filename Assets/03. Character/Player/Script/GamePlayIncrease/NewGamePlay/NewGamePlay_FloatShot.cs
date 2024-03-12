using UnityEngine;
using UnityEngine.Playables;

public class NewGamePlay_FloatShot : NewGamePlay_Basic_FloatShot
{
    //Script
    private NGP_SuperJump superJump;
    private NGP_Shot shot;
    private BulletTime bulletTime;
    private PlayerState playerState;
    [SerializeField] private AudioSource S_floatShot;
    [Space(10)]
    [SerializeField] private int eachShotCount;
    [SerializeField] private float angle_X;
    [SerializeField] private float angle_Y;
    [SerializeField] private float bulletTime_time;

    private void Awake()
    {
        shot = GetComponent<NGP_Shot>();
        superJump = GetComponent<NGP_SuperJump>();
    }
    protected override void Start()
    {
        base.Start();
        bulletTime = GameManager.singleton.GetComponent<BulletTime>();
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void floatShot()
    {
        base.floatShot();
        float x = Random.Range(-angle_X, angle_X);
        float y = Random.Range(-angle_Y, angle_Y);

        for (int i = 0; i < eachShotCount; i++)
        {
            shot.Shot(x, y);
        }
    }
    protected override void OnFloatShotStart()
    {
        base.OnFloatShotStart();

        bulletTime.BulletTime_Slow();
        S_floatShot.Play();
    }
    protected override void OnFloatEnd()
    {
        base.OnFloatEnd();
        playerState.SetGravityToNormal();
        bulletTime.BulletTime_Normal();
        S_floatShot.Stop();
    }
    protected override void OnFloatShotStop()
    {
        base.OnFloatShotStop();

        bulletTime.BulletTime_Normal();
    }
}
