using UnityEngine;

public class Context: MonoBehaviour
{
    private State m_State = null;

    public void Request(int value)
    {
        m_State.Handle(value);
    }

    public void SetState(State theState)
    {
        Debug.Log("Context.SetState: " + theState);
        m_State = theState;
    }
}

public abstract class State
{
    protected Context m_Context = null;

    public State(Context theContext)
    {
        m_Context = theContext;
    }

    public abstract void Handle(int value);
}

public class ConcreteStateA : State
{
    public ConcreteStateA(Context theContext) : base(theContext)
    { }

    public override void Handle(int value)
    {
        Debug.Log("ConcreteStateA.Handle");
        if (value > 10)
            m_Context.SetState(new ConcreteStateB(m_Context));
    }
}

public class ConcreteStateB : State
{
    public ConcreteStateB(Context theContext) : base(theContext)
    { }

    public override void Handle(int value)
    {
        Debug.Log("ConcreteStateB.Handle");
        if (value > 20)
            m_Context.SetState(new ConcreteStateC(m_Context));
    }
}

public class ConcreteStateC : State
{
    public ConcreteStateC(Context theContext) : base(theContext)
    { }

    public override void Handle(int value)
    {
        Debug.Log("ConcreteStateC.Handle");
        if (value > 30)
            m_Context.SetState(new ConcreteStateA(m_Context));
    }
}