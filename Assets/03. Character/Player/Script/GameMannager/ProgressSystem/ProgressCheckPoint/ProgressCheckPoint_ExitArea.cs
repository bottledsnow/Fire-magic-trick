using UnityEngine;

public class ProgressCheckPoint_ExitArea : MonoBehaviour
{
    private ProgressSystem _progressSystem;
    [SerializeField] private ProgressCheckPoint TargetCheckPoint;
    [SerializeField] SenceManager _senceManager;

    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _progressSystem.ProgressCheckPoint = TargetCheckPoint.transform;
            _senceManager.ExitTeachArea();
            Destroy(gameObject,5);
        }
    }
}
