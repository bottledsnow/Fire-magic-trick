using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class NGP_FireSkill_BeaconTPDash : NGP_Basic_FireSkill_BeaconTPDash
{
    [Header("TPDash")]
    [SerializeField] TPDashCollider tpCollider;
    [SerializeField] private float TPDashSpeed;
    [SerializeField] private float newHitCountAddY;
    [SerializeField] private float newHitCountAddXZ;

    //Script
    private PlayerState state;
    private Transform Player;

    //VFX
    private ParticleSystem VFX_TPDash;

    //varaible
    private Transform nullTargetTarget;
    private bool isToNewPosition;
    private Vector3 NewPosition;
    private float deltaSpeed;
    private float deltaStartDistance;
    protected override void Start()
    {
        base.Start();
        state = GameManager.singleton.Player.GetComponent<PlayerState>();
        VFX_TPDash = GameManager.singleton.VFX_List.VFX_TPDash;
        Player = GameManager.singleton.Player;
    }
    protected override void Update() { base.Update(); }
    protected override Transform NullTarget()
    {
        return nullTargetTarget;
    }
    protected override void TPDashStartSetting()
    {
        base.TPDashStartSetting();
        state.SetCollider(false);
        state.SetUseMove(false);
        state.setModel(false);
        VFX_TPDash.Clear();
        VFX_TPDash.Play();
        speed = TPDashSpeed;
        NewPosition = Vector3.zero;
        setIsToNewPosition(false);
        tpCollider.gameObject.SetActive(true);
    }
    
    protected override void MoveToTarget(int index)
    {
        if(isToNewPosition)
        {
            Vector3 player = this.Player.transform.position;
            Vector3 target = NewPosition;
            Vector3 dire = (player - target).normalized;
            float dis = (player - target).magnitude;
            float distance = dis / deltaStartDistance;
            deltaSpeed = speed * distance;
            this.Player.transform.position = Vector3.MoveTowards(player, target, deltaSpeed * Time.deltaTime);
            return;
        }else
        {
            Vector3 Player = this.Player.transform.position;
            Vector3 Target = targets[index].transform.position;
            Vector3 dir = (Player - Target).normalized;

            this.Player.transform.position = Vector3.MoveTowards(Player, Target, speed * Time.deltaTime);
        }
    }
    protected override float distance()
    {
        if(isToNewPosition)
        {
            Vector3 player = this.Player.transform.position;
            Vector3 target = NewPosition;
            float dis = (player - target).magnitude;
            return dis;
        }else
        {
            Vector3 Player = this.Player.transform.position;
            Vector3 Target = targets[index].transform.position;
            float distance = (Player - Target).magnitude;
            return distance;
        }
    }
    protected override void ToNextTarget() { base.ToNextTarget(); }
    protected override void ToNewPosition()
    {
        if (isToNewPosition)
        {
            setIsToNewPosition(false);
            HitCount++;
        }
        else
        {
            base.ToNewPosition();
            setIsToNewPosition(true);
            float x = Random.Range(-newHitCountAddXZ, newHitCountAddXZ);
            float y = newHitCountAddY;
            float z = Random.Range(-newHitCountAddXZ, newHitCountAddXZ);
            Vector3 newPosition = new Vector3(Player.position.x + x, Player.position.y + y, Player.position.z + z);
            deltaStartDistance = (Player.position - newPosition).magnitude;
            NewPosition = newPosition;
        }
    }
    protected override void TPDashEnd()
    {
        base.TPDashEnd();
        state.SetCollider(true);
        state.SetUseMove(true);
        state.setModel(true);
        state.SetVerticalVelocity(12);
        VFX_TPDash.Stop();
        VFX_TPDash.Clear();
        if(nullTargetTarget!=null) Destroy(nullTargetTarget.gameObject);
        tpCollider.gameObject.SetActive(false);
        Debug.Log("TPDashEnd");
    }
    private void setIsToNewPosition(bool value) { isToNewPosition = value; }
    public void setNullTarget(Transform value) { nullTargetTarget = value; }
}
