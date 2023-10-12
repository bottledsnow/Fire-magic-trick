using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Data/Basic Controller/Movement")]
public class D_Move : ScriptableObject
{
    [Header("Move")]
    public float moveSpeed;
    public float sprintSpeed;
    public float SpeedChangeRate = 10.0f;
    [Header("Rotation")]
    [Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;

}
