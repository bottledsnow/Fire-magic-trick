using UnityEngine;

public class pfCharge : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public bool IsFire;
    public GameObject pfHit_Normal;
    public GameObject pfHit_Fire;
    private void Start()
    {
        GiveLifeTime();
    }
    private void Update()
    {
        GiveSpeed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Quaternion Dir = this.transform.rotation;
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyState_Shawn enemyState = collision.gameObject.GetComponent<EnemyState_Shawn>();
            if(enemyState.isFire)
            {
                Instantiate(pfHit_Fire, this.transform.position, Dir);
            } else
            {
                if(IsFire)
                {
                    Instantiate(pfHit_Fire, this.transform.position, Dir);
                }
                else
                {
                    Instantiate(pfHit_Normal, this.transform.position, Dir);
                }
            }
        } else
        {
            if (!IsFire)
            {
                Instantiate(pfHit_Normal, this.transform.position, Dir);
            }
            else
            {
                Instantiate(pfHit_Fire, this.transform.position, Dir);
            }
        }

        Destroy(this.gameObject);
    }
    public void InitializeValue(float speed, float lifeTime, GameObject Hit_Normal, GameObject Hit_Fire)
    {
        Speed = speed;
        LifeTime = lifeTime;
        pfHit_Normal = Hit_Normal;
        pfHit_Fire = Hit_Fire;
    }
    private void GiveSpeed()
    {
        this.transform.position += this.transform.forward * Speed * Time.deltaTime; 
    }
    private void GiveLifeTime()
    {
        Destroy(this.gameObject, LifeTime);
    }
}
