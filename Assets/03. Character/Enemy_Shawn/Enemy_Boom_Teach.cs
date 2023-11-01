using UnityEngine;

public class Enemy_Boom_Teach : MonoBehaviour
{
    [SerializeField] private EnemyHealthSystem_Teach enemyHealthSystesm;
    [HideInInspector]public Rigidbody rb;
    private void Awake()        
    {
        rb = enemyHealthSystesm.GetComponent<Rigidbody>();
    }
    public void Boom()
    {
        enemyHealthSystesm.Boom = true;
    }
}
