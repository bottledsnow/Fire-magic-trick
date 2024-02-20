using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    private bool isTrack;

    private void Start()
    {
        Player = GameManager.singleton.Player.gameObject;
    }
    private void Update()
    {
        if(isTrack)
        {
            RotateToward();
        }
    }
    public void SetTrack(bool active)
    {
        isTrack = active;
    }
    private void RotateToward()
    {
        Vector3 targetRotation = Player.transform.position - transform.position;
        float singlestep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetRotation, singlestep,0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
