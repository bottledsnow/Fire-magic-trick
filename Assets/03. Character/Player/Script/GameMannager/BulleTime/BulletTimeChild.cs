using UnityEngine;

public class BulletTimeChild : MonoBehaviour
{
    private BulletTimeSystem _bulletTimeSystem;
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
        _bulletTimeSystem = GameManager.singleton.GetComponent<BulletTimeSystem>();
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
            _bulletTimeSystem.BulletTimeNormal();

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
                _bulletTimeSystem.BulletTimeSlow();
                break;
            case BulletTimeType.Mid:
                _bulletTimeSystem.BulletTimeMid();
                break;
            case BulletTimeType.NearNormal:
                _bulletTimeSystem.BulletTimeNearNormal();
                break;
            case BulletTimeType.Normal:
                _bulletTimeSystem.BulletTimeNormal();
                break;
        }
    }
}
