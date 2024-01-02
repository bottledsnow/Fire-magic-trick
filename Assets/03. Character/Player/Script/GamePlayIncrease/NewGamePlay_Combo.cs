using System.Threading.Tasks;
using UnityEngine;

public class NewGamePlay_Combo : NewGamePlay_Basic_Combo
{
    private FireDash fireDash;
    public delegate void ComboHandler();
    public event ComboHandler OnUseSkill;
    [Header("Combo")]
    [SerializeField] private float ComboTime;

    protected override void Start()
    {
        base.Start();
        fireDash = GameManager.singleton.EnergySystem.GetComponent<FireDash>();
        fireDash.OnDash += ComboTrigger;
        fireDash.OnDash += UseSkill;
    }
    private void UseSkill()
    {
        OnUseSkill?.Invoke();
    }
}
