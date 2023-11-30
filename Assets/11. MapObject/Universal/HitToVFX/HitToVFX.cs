using UnityEngine;

public class HitToVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem VFX;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Bullet") || collision.collider.CompareTag("ChargeBullet"))
        {
            if(VFX != null)
            {
                VFX.Play();
            }
        }
    }
}
