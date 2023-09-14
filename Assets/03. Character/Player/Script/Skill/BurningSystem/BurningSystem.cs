using UnityEngine;

public class BurningSystem : MonoBehaviour
{
    public float BurningDuration;
    public float BurningDamage;
    public float BurningInterval_ms;
    public float DamageCount;
    private void OnValidate()
    {
        DamageCount = BurningDuration / (BurningInterval_ms / 1000);
    }

}
