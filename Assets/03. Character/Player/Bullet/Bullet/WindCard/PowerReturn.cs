using UnityEngine;

public class PowerReturn : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float absorbDistance = 0.5f;

    //Scritp
    private NGP_SkillPower skillPower;

    //variable
    private Transform player;

    public enum Type
    {
        Wind,
        Fire,
    }
    public Type type;

    private void Start()
    {
        player = GameManager.singleton.Player.transform;
        skillPower = GameManager.singleton.NewGamePlay.GetComponent<NGP_SkillPower>();
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
            if(type == Type.Fire)
            {
                if (skillPower != null) skillPower.AddFirePower();
            }
            else
            if (type == Type.Wind)
            {
                if (skillPower != null) skillPower.AddWindPower();
            }
            Destroy(gameObject);
        }
    }
}
