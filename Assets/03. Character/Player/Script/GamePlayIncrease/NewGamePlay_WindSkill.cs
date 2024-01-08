using UnityEngine;

public class NewGamePlay_WindSkill : NewGamePlay_Basic_WindSkill
{
    [Header("Wind Skill")]
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform circleCard;
    [SerializeField] private Transform circleCardFloat;
    [SerializeField] private Transform circleCardBoom;
    [SerializeField] private Transform cardPlatform;

    [Header("Super Jump")]
    [SerializeField] private float SuperJumpHeight = 7f;

    //Script
    private PlayerJump jump;

    //VFX
    private ParticleSystem VFX_SuperJump;
    public override void Start()
    {
        base.Start();

        jump = GameManager.singleton.Player.GetComponent<PlayerJump>();
        VFX_SuperJump = GameManager.singleton.VFX_List.VFX_SuperJump;

        //Event
        jump.OnJump += VFX_superJump;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void SuperJump()
    {
        base.SuperJump();

        jump.Jump(SuperJumpHeight);
        Debug.Log("Super Jump");
    }
    private void VFX_superJump()
    {
        VFX_SuperJump.Play();
    }
    protected override void CircleCard()
    {
        base.CircleCard();
        Instantiate(circleCard, spawn.position, Quaternion.identity);
    }
    protected override void CircleCardFloat()
    {
        base.CircleCardFloat();
        Instantiate(circleCardFloat, spawn.position, Quaternion.identity);
    }
    protected override void CircleCardBoom()
    {
        base.CircleCardBoom();
        Instantiate(circleCardBoom, spawn.position, Quaternion.identity);
    }
}
