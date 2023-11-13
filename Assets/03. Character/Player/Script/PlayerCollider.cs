using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    //[SerializeField] private bool isHit;
    private CharacterController _characterController;
    public ControllerColliderHit hit;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HitCheck(hit, "Enemy");
        HitCheck(hit, "FirePoint");
    }
    private void HitCheck(ControllerColliderHit hit,string tag)
    {
        if (hit.collider.tag == tag)
        {
            this.hit = hit;
            //isHit = true;
        }
        else
        {
            this.hit = null;
            //isHit = false;
        }
    }

}
