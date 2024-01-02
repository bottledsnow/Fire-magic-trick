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
    public VFX_List VFX_List;

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
