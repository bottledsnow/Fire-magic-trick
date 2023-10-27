using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            EnergySystem energySystem = collision.gameObject.GetComponent<EnergySystem>();
            energySystem.Energy -= damage;
        }
        
        DestroyBullet();
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
