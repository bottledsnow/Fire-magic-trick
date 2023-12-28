using StarterAssets;
using UnityEngine;

public class Shooting_XBoxCameraCheck : MonoBehaviour
{
    [SerializeField] private LayerMask mask = new LayerMask();

    private ThirdPersonController thirdPersonController;

    [Header("Test")]
    public bool isXboxSupport;
    public bool isTrigger;
    public float origionalSensitivity_x;
    public float origionalSensitivity_y;
    public float reduceMultiplier;

    private void Start()
    {
        thirdPersonController = GameManager.singleton.Player.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        ShootRay();
    }
    private void ShootRay()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 999f, mask);

        if (raycastHit)
        {
            hitBoxTarget();
        }
        else
        {
            missedXboxTarget();
        }
    }
    private void hitBoxTarget()
    {
        if(!isTrigger)
        {
            reduceSensitive();
            setIsTrigger(true);
        }
    }
    private void missedXboxTarget()
    {
        if(isTrigger)
        {
            recoverSensitive();
            setIsTrigger(false);
        }
    }
    private void reduceSensitive()
    {
        //Get
        origionalSensitivity_x = getSensitive_X();
        origionalSensitivity_y = getSensitive_Y();

        //New
        float newSensitivity_x = origionalSensitivity_x * reduceMultiplier;
        float newSensitivity_y = origionalSensitivity_y * reduceMultiplier;

        //Set
        thirdPersonController.SetSensitivity(newSensitivity_x, newSensitivity_y);
    }
    private void recoverSensitive()
    {
        //Set
        thirdPersonController.SetSensitivity(origionalSensitivity_x, origionalSensitivity_y);
    }
    private float getSensitive_X()
    {
        return thirdPersonController.Sensitivity_x;
    }
    private float getSensitive_Y()
    {
        return thirdPersonController.Sensitivity_y;
    }
    private void setIsTrigger(bool active)
    {
        isTrigger = active;
    }
}
