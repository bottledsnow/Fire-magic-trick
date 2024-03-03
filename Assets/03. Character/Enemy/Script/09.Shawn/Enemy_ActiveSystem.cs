using BehaviorDesigner.Runtime;
using UnityEngine;

public class Enemy_ActiveSystem : MonoBehaviour
{
    [SerializeField] private bool activeDelay;
    private BehaviorTree tree;
    private Collider coli;
    private Rigidbody rb;

    //variable
    private bool readyToLand;
    private void Awake()
    {
        tree = GetComponent<BehaviorTree>();
        coli = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        if (activeDelay)
        {
            stopEnemy();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer ==9)
        {
            tree.enabled = true;

            if (readyToLand)
            {
                BehaviorTree behaviorTree = GetComponent<BehaviorTree>();
                behaviorTree.SendEvent("HitByPlayer");
            }
        }
    }
    public void activeEnemy()
    {
        coli.enabled = true;
        rb.useGravity = true;

        //ground to move
        readyToLand = true;
    }
    public void stopEnemy()
    {
        //tree.enabled = false;
    }
    public void stopEnemyAll()
    {
        coli.enabled = false;
        rb.useGravity = false;
        //tree.enabled = false;
    }
}
