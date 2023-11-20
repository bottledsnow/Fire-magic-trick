using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public ControllerColliderHit hit;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("FirePoint") || hit.collider.CompareTag("Enemy"))
        {
            this.hit = hit;
        }
        else
        {
            this.hit = null;
        }
    }
    public void ClearColliderTarget()
    {
        hit = null;
    }
}
