using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyDebuff : MonoBehaviour
{
    //Script
    private Rigidbody rb;

    //variable
    private bool isDebuff;
    private Vector3 originScale;
    private float originDrag;
    private float timer;
    private float debuffTime = 5f;
    public enum DebuffType
    {
        None,
        Money,
    }
    private void Start()
    {
        //Script
        rb = GetComponent<Rigidbody>();

        //variable
        originScale = this.transform.localScale;
        originDrag = rb.drag;
    }
    private void Update()
    {
        debuffTimer();
    }
    private void debuffTimer()
    {
        if(isDebuff)
        {
            timer-=Time.deltaTime;
        }

        if(timer <=0)
        {
            Initialization();
            setIsDebuff(false);
        }
    }
    private void Initialization()
    {
        this.transform.localScale = originScale;
        rb.drag = originDrag;
    }

    public void Hit(DebuffType debuffType)
    {
        switch (debuffType)
        {
            case DebuffType.None:
                break;
            case DebuffType.Money:

                //clean aggro
                EnemyAggroSystem enemyAggroSystem = GetComponent<EnemyAggroSystem>();
                if(enemyAggroSystem != null) enemyAggroSystem.CleanAggroTarget();

                //move Slow
                timer = debuffTime;
                rb.drag = 5;

                //¬Ý«ó
                float x = originScale.x * 1f;
                float y = originScale.y * 0.5f;
                float z = originScale.z * 1f;
                this.transform.localScale = new Vector3(x,y,z);

                //set isDebuff
                setIsDebuff(true);
                break;
            default:
                break;
        }
    }
    private void setIsDebuff(bool value) { isDebuff = value; }
}
