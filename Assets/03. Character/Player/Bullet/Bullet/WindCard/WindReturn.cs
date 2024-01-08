using UnityEngine;

public class WindReturn : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float absorbDistance = 0.5f;

    //Scritp
    private NewGamePlay_Basic_Wind newGamePlay_Wind;

    //variable
    private Transform player;

    private void Start()
    {
        player = GameManager.singleton.Player.transform;
        newGamePlay_Wind = GameManager.singleton.NewGamePlay.GetComponent<NewGamePlay_Basic_Wind>();
    }

    private void Update()
    {
        MoveToPlayer();
        absorb();
    }
    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
    private void absorb()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < absorbDistance)
        {
            newGamePlay_Wind.AddWindPower();
            Destroy(gameObject);
        }
    }
}
