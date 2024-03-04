using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class Satun_Laser : MonoBehaviour
{
    private Animator animator;
    private VisualEffect vfx;
    [SerializeField] private Collider Trigger;
    private ParticleSystem[] particle;
    [SerializeField] private ParticleSystem LaserVFX;
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        vfx = GetComponentInChildren<VisualEffect>();
        particle = GetComponentsInChildren<ParticleSystem>();
    }
    private void Start()
    {
        ActiveParticle(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (animator != null)
        {
            if(other.CompareTag("Player"))
            {
                LaserVFX.Play();
                vfx.Play();
                Laser();
            }
        }
    }
    public void active(bool state)
    {
        ActiveParticle(state);

        if(state)
        {
            animator.SetTrigger("Start");
        }
        else
        {
            animator.SetTrigger("End");
            animator.Play("Idel");
        }
    }
    private async void Laser()
    {
        Trigger.gameObject.SetActive(true);
        await Task.Delay(250);
        Trigger.gameObject.SetActive(false);
    }
    private void ActiveParticle(bool active)
    {
        for (int i = 0; i < particle.Length; i++)
        {
            if(active)
            {
                particle[i].Play();
            }else
            {
                particle[i].Stop();
            }
        }
    }
}
