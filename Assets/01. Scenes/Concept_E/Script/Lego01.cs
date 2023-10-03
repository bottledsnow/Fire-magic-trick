using UnityEngine;

public class Lego01 : MonoBehaviour
{
    [Header("rotate")]
    public float x;
    public float y;
    public float z;
    void Update()
    {
        RotateLego();
    }

    private void RotateLego()
    {
        float newx = x* Time.deltaTime;
        float newy = y* Time.deltaTime;
        float newz = z* Time.deltaTime;
        transform.Rotate(newx,newy,newz);
    }
}
