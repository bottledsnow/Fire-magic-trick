using UnityEngine;

public class TestDelegate : MonoBehaviour
{
    public delegate void TestDelegateHandler();
    public event TestDelegateHandler TestDelegateEvent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TestDelegateEvent?.Invoke();
        }
    }
}
