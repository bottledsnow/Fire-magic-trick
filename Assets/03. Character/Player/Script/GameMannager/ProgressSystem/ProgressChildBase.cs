using UnityEngine;

public class ProgressChildBase : MonoBehaviour
{
    private ProgressSystem _progressSystem;

    private void Start()
    {
        _progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();

        Initialization();
    }

    private void Initialization()
    {
        _progressSystem.OnPlayerDeath += DoSomething;
    }
    protected virtual void DoSomething()
    {

    }
}
