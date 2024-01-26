using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private Color Normal;
    [SerializeField] private Color Check;
    private Material CrosshairMaterial;
    private Animator CrosshairAnimator;
    [Header("Hit")]
    [SerializeField] private float thresholdDistance;
    [SerializeField] private MMF_Player hitNear;
    [SerializeField] private MMF_Player hitFar;

    //interface
    private IHitNotifier hitNotifier;

    private void Start()
    {
        CrosshairAnimator = Crosshair.GetComponent<Animator>();
        CrosshairMaterial = Crosshair.GetComponent<Image>().material;
    }
    public void SuperDashCheck()
    {
        CrosshairMaterial.color = Check;
    }
    public void CrosshairInitialization()
    {
        CrosshairMaterial.color = Normal;
    }
    public void CrosshairShooting()
    {
        CrosshairAnimator.Play("Crosshair");
    }
    public void CrosshairHit()
    {
        CrosshairAnimator.Play("CrossHairHit");
    }
    public void EnemyHitImpluse(Vector3 hitPosition)
    {
        float distance = Vector3.Distance(hitPosition, transform.position);
        if(distance < thresholdDistance)
        {
            hitNear.PlayFeedbacks();
        }else
        {
              hitFar.PlayFeedbacks();
        }
    }
}
