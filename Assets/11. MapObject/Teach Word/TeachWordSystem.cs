using UnityEngine;

public class TeachWordSystem : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenTeachWord()
    {
        animator.Play("Open");
    }
    public void CloseTeachWord()
    {
        animator.Play("Close");
    }
}
