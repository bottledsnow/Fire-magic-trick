using MoreMountains.Feedbacks;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager singleton = null;
    public ControllerInput _input;
    public PlayerState _playerState;
    public GameObject UISystem;
    public GameObject EnergySystem;
    public GameObject ShootingSystem;
    public Transform Player;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }
    private void Start()
    {
        Player = _input.gameObject.transform;
    }
}
