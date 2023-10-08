using UnityEngine;

public class EnemyState_Shawn : MonoBehaviour
{
    public bool isFire;

    [SerializeField] private GameObject VFX_Fire;
    private void Update()
    {
        easyFire();
    }

    private void easyFire()
    {
        if (isFire)
        {
            VFX_Fire.SetActive(true);
        }else
        {
            VFX_Fire.SetActive(false);
        }
    }
}
