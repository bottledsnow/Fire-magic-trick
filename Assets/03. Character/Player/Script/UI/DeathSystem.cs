using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void EnterDeathImage()
    {
        animator.Play("Enter");
    }
    public void EnterDeathImage_Fast()
    {
        animator.Play("Enterfast");
    }
    public void ExitDeathImage()
    {
        animator.Play("Exit");
    }
}
