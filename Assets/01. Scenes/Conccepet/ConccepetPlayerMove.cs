using UnityEngine;

public class ConccepetPlayerMove : MonoBehaviour
{
    public float horizontal;
    public float vertical;

    private void Update()
    {
        Horizontal();
    }

    private void Horizontal()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}
