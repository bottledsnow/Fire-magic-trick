using UnityEngine;

public class NGP_Basic_FireSkill_BeaconTPDash : MonoBehaviour
{
    //Script
    protected GameObject[] targets = new GameObject[5];
    protected NGP_SkillPower skillPwer;
    protected virtual void Start()
    {
        skillPwer = GameManager.singleton.NewGamePlay.GetComponentInParent<NGP_SkillPower>();
    }
    public void ToUpdateTarget(GameObject[] targets)
    {
        this.targets = targets;
        skillPwer.UseFire();
    }
}
