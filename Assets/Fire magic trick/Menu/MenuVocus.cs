using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVocus : MonoBehaviour
{
    private Animator animator;
    public int Version;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        toWhere(Version);
    }

    private void toWhere(int version)
    {
        if(version==0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.Play("ToRed");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.Play("ToOrange");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("ToYellow");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.Play("ToMenu");
            }
        }
        if(version==1)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.Play("ToRed");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                animator.Play("ToOrange");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.Play("ToYellow");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("ToMenu");
            }
        }
    }
}
