using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private Color Normal;
    [SerializeField] private Color Check;
    private Material CrosshairMaterial;
    private Animator CrosshairAnimator;

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
}
