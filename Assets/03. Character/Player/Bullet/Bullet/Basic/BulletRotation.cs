using UnityEngine;

public class BulletRotation : MonoBehaviour
{
    private RotateSystem rotateSystem;
    [SerializeField] private Vector3 rotateSpeed_Min;
    [SerializeField] private Vector3 rotateSpeed_Max;

    private void Start()
    {
        rotateSystem = GetComponent<RotateSystem>();

        float x = Random.Range(rotateSpeed_Min.x, rotateSpeed_Max.x);
        float y = Random.Range(rotateSpeed_Min.y, rotateSpeed_Max.y);
        float z = Random.Range(rotateSpeed_Min.z, rotateSpeed_Max.z);
        Vector3 rotateSpeed = new Vector3(x, y, z);
        rotateSystem.SetRotateSpeed(rotateSpeed);
    }
    public void OnTrack()
    {
        rotateSystem.SetUseRotate(false);
    }
}
