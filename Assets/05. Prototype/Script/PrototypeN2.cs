using UnityEngine;

public class PrototypeN2 : MonoBehaviour
{
    [SerializeField] private GameObject Enermy;
    [SerializeField] private GameObject EnermyWeakness;
    [SerializeField] private BrokeGlass BrokenGlass;
    [SerializeField] private PrototypeN3 prototypeN3;

    public void isFalling()
    {
        prototypeN3.enabled = true;
        prototypeN3.isFalling = true;
    }
}
