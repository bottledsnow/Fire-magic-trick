using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] private float Energy;

    [Header("Movement")]
    [SerializeField] private float StopTime;
    [SerializeField] private float speed_Start = 100;
    [SerializeField] private float rotateDeviation = 60;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    [Header("NearbyDetect")]
    [SerializeField] private float movementStartDistance;
    [SerializeField] private float absorbDistance;

    private float timer;
    private bool isTimer;
    private bool isTimerFinish;

    private EnergySystem energySystem;
    private GameObject EnemrgyBall;
    private GameObject player;
    private Rigidbody rb;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemrgyBall = this.gameObject;
        energySystem = GameManager.singleton.Player.GetComponent<EnergySystem>();
        rb = GetComponent<Rigidbody>();

        startMove(speed_Start);
        SetIsTimer(true);
    }
    private void Update()
    {
        CheckBallMoveToPlayerDistance();
        CheckPlayerAbsorbDistance();
        timerSystem();
    }
    private void timerSystem()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
        }

        if(timer > StopTime)
        {
            SetIsTimer(false);
            SetIsTimerFinish(true);
        }
    }
    private void startMove(float speed)
    {
        float x = Random.Range(-rotateDeviation, rotateDeviation);
        float y = Random.Range(-rotateDeviation, rotateDeviation);
        float z = Random.Range(-rotateDeviation, rotateDeviation);
        Quaternion rotate = Quaternion.Euler(x, y, z);
        rb.gameObject.transform.rotation = rotate;
        rb.drag = 3;
        rb.AddForce(transform.up * speed,ForceMode.Impulse);
    }
    private void CheckBallMoveToPlayerDistance()
    {
        if (distanceToPlayer() <= movementStartDistance)
        {
            moveToPlayer();
        }
    }
    private void CheckPlayerAbsorbDistance()
    {
        if (distanceToPlayer() <= absorbDistance)
        {
            giveEnergyToPlayer();
            Destroy(gameObject);
        }
    }
    private void recoverPlayerEnergy(float value)
    {
        energySystem.GetEnergy(value);
    }
    private void moveToPlayer()
    {
        if(isTimerFinish)
        {
            rb.drag = 0;
            Vector3 Direction = player.transform.position - EnemrgyBall.transform.position;
            rb.velocity = (Direction.normalized * speed);
        }
    }
    private void giveEnergyToPlayer()
    {
        recoverPlayerEnergy(Energy);
    }
    private float distanceToPlayer()
    {
        Vector3 distanceVector = player.transform.position - EnemrgyBall.transform.position;
        float distanceToPlayer = distanceVector.magnitude;
        return distanceToPlayer;
    }
    private void SetIsTimer(bool value)
    {
        isTimer = value;
    }
    private void SetIsTimerFinish(bool value)
    {
        isTimerFinish = value;
    }
}
