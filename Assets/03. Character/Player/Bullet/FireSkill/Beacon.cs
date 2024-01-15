using UnityEngine;

public class Beacon : Bullet
{
    private GameObject[] targets = new GameObject[4];
    protected override void Start() { base.Start(); }
    protected override void Update()
    {
        base.Update();
        addBeaconScale();
    }
    public void StopRightNow()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        NGP_FireSkill_BeaconTPDash tpdash = GameManager.singleton.NewGamePlay.GetComponentInParent<NGP_FireSkill_BeaconTPDash>();
        tpdash.ToTPDash(targets);
        NGP_ChargeSkill skill = GameManager.singleton.NewGamePlay.GetComponent<NGP_ChargeSkill>();
        skill.ChargeStopRightNow();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Enemy"))
        {
            for(int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                {
                    targets[i] = other.gameObject;
                    break;
                }
            }
        }
    }
    private void addBeaconScale()
    {
        this.transform.localScale += new Vector3(0.1f, 0.1f, 0);
    }
}
