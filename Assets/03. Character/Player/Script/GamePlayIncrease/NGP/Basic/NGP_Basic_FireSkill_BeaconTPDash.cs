using UnityEngine;

public class NGP_Basic_FireSkill_BeaconTPDash : MonoBehaviour
{
    protected GameObject[] targets = new GameObject[5];

    public void ToUpdateTarget(GameObject[] targets)
    {
        this.targets = targets;
    }
}
