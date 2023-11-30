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
    private GameObject EnemrgyBall;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemrgyBall = this.gameObject.transform.parent.gameObject;
    }
    private void Update()
    {
        CheckBallMoveToPlayerDistance();
        CheckPlayerAbsorbDistance();
    }
    private void CheckBallMoveToPlayerDistance()
    {
        if (distanceToPlayer() <= movementStartDistance)
        {
            MoveToPlayer();
        }
    }
    private void CheckPlayerAbsorbDistance()
    {
        if (distanceToPlayer() <= absorbDistance)
        {
            GiveEnergyToPlayer();
            Destroy(gameObject);
        }
    }

    private void MoveToPlayer()
    {
        Vector3 Direction = player.transform.position - EnemrgyBall.transform.position;
        EnemrgyBall.transform.Translate(Direction.normalized * speed * Time.deltaTime);
        speed += acceleration;
    }
    private float distanceToPlayer()
    {
        Vector3 distanceVector = player.transform.position - EnemrgyBall.transform.position;
        float distanceToPlayer = distanceVector.magnitude;
        return distanceToPlayer;
    }
    private void GiveEnergyToPlayer()
    {
        EnergySystem energySystem = player.GetComponent<EnergySystem>();
        energySystem.Energy += recovery;
    }
}
