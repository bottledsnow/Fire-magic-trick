using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnergy : MonoBehaviour
{
    [Header("EnergyRecovery")]
    [SerializeField] private float recovery;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    [Header("NearbyDetect")]
    [SerializeField] private float movementStartDistance;
    [SerializeField] private float absorbDistance;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(distanceToPlayer() <= movementStartDistance)
        {
            MoveToPlayer();
        }

        if(distanceToPlayer() <= absorbDistance)
        {
            GiveEnergyToPlayer();
            Destroy(gameObject);
        }
    }
    void MoveToPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        transform.Translate(directionToPlayer.normalized * speed * Time.deltaTime);
        speed += acceleration;
    }
    float distanceToPlayer()
    {
        Vector3 distanceVector = player.transform.position - transform.position;
        float distanceToPlayer = distanceVector.magnitude;
        return distanceToPlayer;
    }
    void GiveEnergyToPlayer()
    {
        EnergySystem energySystem = player.GetComponent<EnergySystem>();
        energySystem.Energy += recovery;
    }
}
