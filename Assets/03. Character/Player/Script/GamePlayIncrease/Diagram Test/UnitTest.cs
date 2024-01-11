using UnityEngine;

public class UnitTest : MonoBehaviour
{
    [SerializeField] private Context context;
    private void Start()
    {
        UnitTest_implement();
    }
    private void UnitTest_implement()
    {
        Context context = new Context();
        context.SetState(new ConcreteStateA(context));

        context.Request(5);
        context.Request(15);
        context.Request(25);
        context.Request(35);
    }
}