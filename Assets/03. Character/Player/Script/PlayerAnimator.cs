using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
