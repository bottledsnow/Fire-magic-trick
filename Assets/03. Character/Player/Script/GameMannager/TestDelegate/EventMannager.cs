using UnityEngine;

public class EventMannager
{
    public delegate void PlayerDeath();
    public event PlayerDeath OnPlayerDeath;

    public bool isPlayerDeah = false;
    
}
