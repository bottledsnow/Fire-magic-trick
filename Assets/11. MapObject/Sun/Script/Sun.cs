using UnityEngine;
using System.Threading.Tasks;

public class Sun : MonoBehaviour
{
    [SerializeField] private GameObject EnergyBall;
    [SerializeField] private float sunSpawnTime = 3;
    [SerializeField] private int MaxSunEnegyBallNumber = 3;
    [Header("VFX")]
    [SerializeField] private GameObject sunVFX;

    private GameObject[] sunEnergyBall;
    private void Start()
    {
        sunEnergyBall = new GameObject[MaxSunEnegyBallNumber];
        InvokeRepeating("spawnEnergyBall", 0, sunSpawnTime);
    }
    private async void SunSpawnTiemr()
    {
        await Task.Delay((int)(sunSpawnTime * 1000));
        spawnEnergyBall();
    }
    private void spawnEnergyBall()
    {
        for (int i = 0; i < MaxSunEnegyBallNumber; i++)
        {
            if (sunEnergyBall[i] == null)
            {
                sunEnergyBall[i] = Instantiate(EnergyBall, transform.position, Quaternion.identity);
                sunEnergyBall[i].transform.parent = transform;
                break;
            }
        }
    }
    public void SetSunVFX(bool active)
    {
        sunVFX.SetActive(active);
    }
}
