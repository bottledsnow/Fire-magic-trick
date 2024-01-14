using UnityEngine;

public class Beacon : Bullet
{
    private GameObject[] target = new GameObject[5];
    protected override void Start() { base.Start(); }
    protected override void Update()
    {
        base.Update();
        addBeaconScale();
    }
    private void OnDestroy()
    {
        NGP_FireSkill_BeaconTPDash tpdash = GameManager.singleton.NewGamePlay.GetComponentInParent<NGP_FireSkill_BeaconTPDash>();
        tpdash.ToUpdateTarget(target);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Enemy"))
        {
            for(int i = 0; i < target.Length; i++)
            {
                if (target[i] == null)
                {
                    target[i] = other.gameObject;
                    break;
                }
            }
        }
    }
    private void addBeaconScale()
    {
        this.transform.localScale += new Vector3(0.1f, 0, 0);
    }
}
