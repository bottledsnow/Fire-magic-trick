using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    public UnityEvent SuperDashKickEvent;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SuperDashKick()
    {
        SuperDashKickEvent?.Invoke();
    }
    public void SuperDashStart()
    {
        animator.SetBool("isSuperDash", true);
    }
    public void SuperDashEnd()
    {
        animator.SetBool("isSuperDash", false);
    }
    public void InputY()
    {
        animator.SetTrigger("InputY");
    }
    public void PlayAnimator(string name)
    {
        animator.Play(name);
    }
    public void ToDialogue_Idel()
    {
        animator.Play("InStory(Idel)");
    }
    public void EndDialogue()
    {
        animator.SetTrigger("EndDialogue");
    }
    public void Dash()
    {
        animator.SetTrigger("Dash");
    }
}
