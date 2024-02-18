using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RepeatFunction : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnRepeat;
    public float countDowntiem;

    [Header("Setting")]
    [SerializeField] private bool isOnce;

    private float timer;
    private bool isTrigger;

    private void Start()
    {
        OnStart.Invoke();
    }
    private void Update()
    {
        timerSystem();
    }
    private void timerSystem()
    {
        if(isOnce) 
        {
            if(isTrigger)
            {
                return;
            }
        }

        if(timer < countDowntiem)
        {
            timer+=Time.deltaTime;
        }

        if(timer > countDowntiem)
        {
            OnRepeat.Invoke();
            OnStart.Invoke();
            isTrigger = true;
            timer = 0;
        }
    }
}
