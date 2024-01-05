public interface IHitNotifier
{
    event MyDelegates.OnHitHandler OnHit;
}
public interface ITriggerNotifier
{
    event MyDelegates.OnTriggerHandler OnTrigger;
}
