using UnityEngine;

public class NewGamePlay_FloatShot : NewGamePlay_Basic_FloatShot
{
    private NGP_Shot shot;
    private BulletTime bulletTime;
    [SerializeField] private AudioSource S_floatShot;
    [Space(10)]
    [SerializeField] private int eachShotCount;
    [SerializeField] private float angle_X;
    [SerializeField] private float angle_Y;
    [SerializeField] private float bulletTime_time;
    protected override void Start()
    {
        base.Start();

        shot = GetComponent<NGP_Shot>();
        bulletTime = GameManager.singleton.GetComponent<BulletTime>();
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

        bulletTime.BulletTime_Normal();
        S_floatShot.Stop();
    }
    protected override void OnFloatShotStop()
    {
        base.OnFloatShotStop();

        bulletTime.BulletTime_Normal();
    }
}
