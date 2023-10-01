using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private bool isHit;
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
            isHit = true;
        } else
        {
            this.hit = null;
            isHit = false;
        }
    }

}
