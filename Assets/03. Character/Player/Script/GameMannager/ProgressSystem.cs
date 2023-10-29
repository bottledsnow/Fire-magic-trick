using UnityEngine;

public class ProgressSystem : MonoBehaviour
{
    public Transform ProgressCheckPoint;

    private DeathSystem _deathSystem;
    private Transform player;

    private void Start()
    {
        _deathSystem = GameManager.singleton.UISystem.GetComponent<DeathSystem>();
        player = GameManager.singleton.Player;
    }
    
    public void PlayerDeath()
    {
        if(ProgressCheckPoint != null)
        {
            player.transform.position = ProgressCheckPoint.position;
            PlayerRebirth();
        }
    }
    public void PlayerRebirth()
    {
        _deathSystem.ExitDeathImage();
    }
}
