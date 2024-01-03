using UnityEngine;

public class BulletTimeChild : MonoBehaviour
{
    private BulletTime _bulletTimeSystem;
    public enum BulletTimeType
    {
        Slow,
        Mid,
        NearNormal,
        Normal
    }
    [SerializeField] private bool isOneShot;
    public BulletTimeType bulletTimeType;

    private bool Trigger;

    private void Start()
    {
        _bulletTimeSystem = GameManager.singleton.GetComponent<BulletTime>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ChangeTimeScale();
            Trigger = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !Trigger)
        {
            ChangeTimeScale();
            Trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _bulletTimeSystem.BulletTime_Normal();

            if(!isOneShot)
            {
                Trigger = false;
            }
        }
    }
    private void ChangeTimeScale()
    {
        switch (bulletTimeType)
        {
            case BulletTimeType.Slow:
                _bulletTimeSystem.BulletTime_Slow();
                break;
            case BulletTimeType.Mid:
                _bulletTimeSystem.BulletTime_Mid();
                break;
            case BulletTimeType.NearNormal:
                _bulletTimeSystem.BulletTime_NearNormal();
                break;
            case BulletTimeType.Normal:
                _bulletTimeSystem.BulletTime_Normal();
                break;
        }
    }
}
