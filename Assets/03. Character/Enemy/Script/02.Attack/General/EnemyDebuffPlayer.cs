using UnityEngine;

public class EnemyDebuffPlayer : MonoBehaviour
{
    [SerializeField] private float debuffTime;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DebuffPlay(collider);
        }
    }
    private void DebuffPlay(Collider collider)
    {
        GameObject player = collider.gameObject;
        PlayerState playerState = player.GetComponent<PlayerState>();

        playerState.DebuffPlay(debuffTime);
    }

}
