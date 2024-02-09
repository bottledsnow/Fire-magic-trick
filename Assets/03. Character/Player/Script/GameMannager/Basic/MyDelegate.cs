using UnityEngine;
public class MyDelegates
{
    public delegate void OnHandler();
    public delegate void OnTriggerHandler(Collider other);
    public delegate void OnHitHandler(Collision collision);
    public delegate void OnHitTriggerHandler(Collider other);
}