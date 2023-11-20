using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator_A : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private EnemyHealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponent<EnemyHealthSystem>();
        _healthSystem.OnEnemyHit += AnimatorHurt;
    }

    private void AnimatorHurt()
    {
          animator.SetTrigger("Hurt");
    }
}
