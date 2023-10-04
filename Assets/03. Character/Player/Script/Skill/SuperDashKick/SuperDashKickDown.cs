using UnityEngine;

public class SuperDashKickDown : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private float kickDownForce;
    public void GetTarget(GameObject target)
    {
        Target = target;
    }
    public void NullTarget()
    {
        Target = null;
    }
    public void KickDown()
    {
        if(Target != null)
        {
            Enemy_Boom enemy_Boom = Target.GetComponent<Enemy_Boom>();
            Rigidbody rb = enemy_Boom.rb;
            enemy_Boom.Boom();
            Vector3 rbUp = rb.gameObject.transform.up;
            rb.velocity = -rbUp * kickDownForce;
            
        }
    }
}
