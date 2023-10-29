using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void EnterDeathImage()
    {
        _animator.Play("Enter");
    }
    public void ExitDeathImage()
    {
        _animator.Play("Exit");
    }
}
