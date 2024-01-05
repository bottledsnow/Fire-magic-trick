using UnityEngine;

public class RotateToWardTest : MonoBehaviour
{
    public GameObject Target;
    public float speed;
    private void Update()
    {
        RotateToward();
    }

    private void RotateToward()
    {
        Vector3 targetRotation = Target.transform.position - transform.position;
        float singlestep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetRotation, singlestep,0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
