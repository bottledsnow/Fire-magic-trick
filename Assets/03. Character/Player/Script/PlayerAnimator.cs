using BehaviorDesigner.Runtime.Tasks;
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
    public void ToDialogue_Idel()
    {
        _playerAnimator.Play("InStory(Idel)");
    }
    public void EndDialogue()
    {
        _playerAnimator.SetTrigger("EndDialogue");
    }

}
