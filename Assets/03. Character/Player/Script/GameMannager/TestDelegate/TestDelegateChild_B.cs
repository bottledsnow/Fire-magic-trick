using UnityEngine;

public class TestDelegateChild_B : MonoBehaviour
{
    private TestDelegate testDelegate;
    private void Start()
    {
        testDelegate = GetComponent<TestDelegate>();

        testDelegate.TestDelegateEvent += DoSomwthing;
    }

    private void DoSomwthing()
    {
        Debug.Log("Delegate child B");
    }
}
