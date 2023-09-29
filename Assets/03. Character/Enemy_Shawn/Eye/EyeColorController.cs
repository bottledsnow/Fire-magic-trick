using UnityEngine;

public class EyeColorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetYellow()
    {
        animator.SetTrigger("Yellow");
    }
    public void SetOrange()
    {
        animator.SetTrigger("Orange");
    }
    public void SetRed()
    {
        animator.SetTrigger("Red");
    }
    public void SetPurple()
    {
        animator.SetTrigger("Purple");
    }
}
