using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
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
        if (hit.collider.tag =="Enemy")
        {
            this.hit = hit;
        } else
        {
            this.hit = null;
        }
    }

}
