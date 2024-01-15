using UnityEngine;
using UnityEngine.Networking.Types;

public class NGP_Basic_FireSkill_BeaconTPDash : MonoBehaviour
{
    //Script
    protected GameObject[] targets = new GameObject[4];
    protected NGP_SkillPower skillPwer;
    protected NGP_ChargeSkill chargeSkill;

    //variable
    protected float speed;
    protected int HitCount;
    protected int index;
    private bool isTPDash;

    protected virtual void Start()
    {
        //Script
        skillPwer = GameManager.singleton.NewGamePlay.GetComponentInParent<NGP_SkillPower>();
        chargeSkill = GameManager.singleton.NewGamePlay.GetComponentInParent<NGP_ChargeSkill>();

        //variable
        index = 0;
    }
    protected virtual void Update()
    {
        TPDashSystem();
    }
    public void ToTPDash(GameObject[] targets)
    {
        index = 0; //Reset index
        this.targets = targets; //Update targets
        HitCount = chargeSkill.GetChargeCount() + 2;  //caculate HitCount
        TPDashStartSetting(); //Setting for TPDash
        setIsTPDash(true); //Trigger TPDash
        skillPwer.UseFire(); //Use Fire
    }
    protected virtual void TPDashStartSetting() { }
    private void TPDashSystem()
    {
        if(isTPDash)
        {
            if (targets[index] != null) MoveToTarget(index);

            if (distance() <= 0.5f)
            {
                ToNextTarget(); //index++ and HitCount--

                if (targets[index] == null)
                {
                    index--;
                    if(HitCount < 0)
                    {
                        TPDashEnd();
                        Debug.Log("Hitcoun:"+ HitCount+"TEPDashEnd");
                    }
                    else
                    {
                        ToNewPosition();
                    }
                }
            }
        }
    }
    protected virtual void MoveToTarget(int index) { }
    protected virtual void ToNextTarget() { index++; HitCount--; }
    protected virtual void ToNewPosition() { }
    protected virtual void TPDashEnd() { setIsTPDash(false); }
    protected virtual float distance() { return 0; }
    private void setIsTPDash(bool value) { isTPDash = value; }
}
