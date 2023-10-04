using UnityEngine;

public class Enemy_Boom : MonoBehaviour
{
    [SerializeField] private Enemy_Shawn enemy_Shawn;
    public Rigidbody rb;
    private void Awake()
    {
        rb = enemy_Shawn.GetComponent<Rigidbody>();
    }
    public void Boom()
    {
        enemy_Shawn.Boom = true;
    }
}
