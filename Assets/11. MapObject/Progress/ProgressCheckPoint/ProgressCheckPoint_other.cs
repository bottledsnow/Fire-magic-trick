using UnityEngine;

public class ProgressCheckPoint_other : MonoBehaviour
{
    private ProgressSystem _progressSystem;
    [SerializeField] private ProgressCheckPoint _progressCheckPoint;
    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _progressSystem.ProgressCheckPoint = _progressCheckPoint.transform;
        }
    }
}
