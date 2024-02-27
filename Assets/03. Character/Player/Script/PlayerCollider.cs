using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public ControllerColliderHit hit;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HitGlass(hit);

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
    private void HitGlass(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Glass"))
        {
            GlassSystem glass = hit.collider.GetComponent<GlassSystem>();
            if(glass != null)
            {
                glass.Broken();
            }
        }
    }
}
