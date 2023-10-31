using UnityEngine;

public class LimitForTeachChild : MonoBehaviour
{
    public enum LimitType
    {
        Float,
        Dash,
        SuperDash,
        Magazing,
        ShootingMode,
    }
    public LimitType limitType; 

    private LimitForTeach _limitForTeach;

    private void Start()
    {
        _limitForTeach = GameManager.singleton.GetComponent<LimitForTeach>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(limitType)
            {
                case LimitType.Float:
                    _limitForTeach.SetUI_EnergySystem(true);
                    _limitForTeach.SetFloatingScript(true);
                    break;
                case LimitType.Dash:
                    _limitForTeach.SetDashScript(true);
                    break;
                case LimitType.SuperDash:
                    _limitForTeach.SetSuperDashScript(true);
                    break;
                case LimitType.Magazing:
                    _limitForTeach.SetUI_ShootingMagazing(true);
                    _limitForTeach.SetBulletMagazingScript(true);
                    break;
                case LimitType.ShootingMode:
                    _limitForTeach.SetUI_ShootingMode(true);
                    _limitForTeach.SetShootingModeScript(true);
                    break;
            }
        }
    }
}
