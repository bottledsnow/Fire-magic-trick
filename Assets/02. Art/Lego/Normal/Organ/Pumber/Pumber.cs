using MoreMountains.Tools;
using System.Threading.Tasks;
using UnityEngine;

public class Pumber : MonoBehaviour
{
    //Script
    private Animator animator;

    //
    [SerializeField] private GameObject Trigger;
    [SerializeField] private int State;
    [Header("Death")]
    public bool isDeathPumber;
    [Header("Bounce")]
    public bool isBounce;
    public float SuperPumberForce;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Trigger.SetActive(false);
    }
    private void Start()
    {
        SetAnimatorState(State);
    }
    public void SetAnimatorState(int level)
    {
        animator.SetInteger("State", level);
    }
    public async void SuperPumber()
    {
        Trigger.SetActive(true);
        await Task.Delay(250);
        Trigger.SetActive(false);
    }
    public void SuperPumberReady()
    {
        animator.Play("Pumber Ready Super");
    }
    public void SuperBounce()
    {
        if(isBounce)
        {
            animator.Play("Pumber bounce");
        }
    }
}
