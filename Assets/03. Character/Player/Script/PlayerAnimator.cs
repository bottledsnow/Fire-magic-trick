using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }
    public void SuperDashStart()
    {
        _playerAnimator.SetBool("isSuperDash", true);
    }
    public void SuperDashEnd()
    {
        _playerAnimator.SetBool("isSuperDash", false);
    }
    public void InputY()
    {
        _playerAnimator.SetTrigger("InputY");
    }
    public void PlayAnimator(string name)
    {
        _playerAnimator.Play(name);
    }

}
