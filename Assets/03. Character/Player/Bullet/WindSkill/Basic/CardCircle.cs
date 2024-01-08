using UnityEngine;

public class CardCircle : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameManager.singleton.Player;
        SetParent();
    }
    private void SetParent()
    {
        this.transform.SetParent(player);
    }
}
