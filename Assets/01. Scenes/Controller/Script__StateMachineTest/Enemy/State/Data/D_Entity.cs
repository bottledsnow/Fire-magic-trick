using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Entity")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistatnce = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public LayerMask whatIsGround;
}
