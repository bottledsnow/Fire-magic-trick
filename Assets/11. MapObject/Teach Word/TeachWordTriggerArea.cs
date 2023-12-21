using MoreMountains.Feedbacks;
using UnityEngine;

public class TeachWordTriggerArea : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool isOpen;
    [SerializeField] private bool isClose;
    [Header("Target")]
    [SerializeField] private TeachWordSystem teachWordSystem;


    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenTriggerTeach();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CloseTriggerTeach();
        }
    }

    private void OpenTriggerTeach()
    {
        if(isOpen)
        {
            teachWordSystem.OpenTeachWord();
        }
    }
    private void CloseTriggerTeach()
    {
        if(isClose)
        {
            teachWordSystem.CloseTeachWord();
        }
    }
}
