using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
    private enum ToWheres
    {
        Menu=0,
        Red=1,
        Orange=2,
        Yellow=3
    }
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            toWhere(ToWheres.Red);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            toWhere(ToWheres.Orange);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            toWhere(ToWheres.Yellow);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            toWhere(ToWheres.Menu);
        }
    }
    
    private void toWhere(ToWheres towheres)
    {
        if(towheres==ToWheres.Menu)
        {
            animator.SetTrigger("ToMenu");
        }
        if(towheres==ToWheres.Orange)
        {
            animator.SetTrigger("ToOrange");
        }
        if(towheres==ToWheres.Red)
        {
            animator.SetTrigger("ToRed");
        }
        if(towheres==ToWheres.Yellow)
        {
            animator.SetTrigger("ToYellow");
        }
    }
}
