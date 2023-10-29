using UnityEngine;

public class ProgressCheckPoint : MonoBehaviour
{
    private ProgressSystem _progressSystem;

    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _progressSystem.ProgressCheckPoint = transform;
        }
    }
}
