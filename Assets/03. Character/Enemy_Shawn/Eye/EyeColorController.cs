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
        if (animator != null)
        {
            animator.SetTrigger("Yellow");
        }
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
        if(animator !=null)
        {
            animator.SetTrigger("Purple");
        }
    }
}
