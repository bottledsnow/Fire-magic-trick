using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class BossStage
{
    public string eventName;
    public bool onlyOne;
    public bool isTrigger;
    public float stage_OnHealth;
    public UnityEvent stageEvent;
}
public class Boss : MonoBehaviour, IHealth
{
    public float MaxHealth;
    public float health;
    public bool invincible;
    public bool isWin;
    public BossStage[] stages;
    public UnityEvent OnBossDeath;
    private Boss_System system;
    public int iHealth { get; set; }
    protected virtual void Awake()
    {
        system = GetComponent<Boss_System>();
    }
    protected virtual void Start()
    {
        health = MaxHealth;
    }
    protected virtual void Update()
    {
        CheckActive();
    }
    private void CheckActive()
    {
        if(health >0)
        {
            if(!isWin)
            {
                if(this.gameObject.activeSelf==false)
                {
                    this.gameObject.SetActive(true);
                }
            }
        }
    }
    public void CheckBoosEndAgain()
    {
        if(health>0)
        {
            health = 0;
            OnBossDeath?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
    public virtual void ResetBossFight()
    {
        health = MaxHealth;
        system.SetHealth(healthPersentage(health));

        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i].isTrigger)
            {
                stages[i].isTrigger = false;
            }
        }
    }
    public void TakeDamage(int damage, PlayerDamage.DamageType damageType)
    {
        health -= damage;

        if(health <=0)
        {
            Death();
            return;
        }
        if(health >0)
        {
            if(this.gameObject.activeSelf == false)
            {
                this.gameObject.SetActive(true);
            }
        }

        system.SetHealth(healthPersentage(health));
        StateCheck();
    }
    private void Death()
    {
        system.EndBossFight();
        OnBossDeath?.Invoke();
        isWin = true;
    }
    private void StateCheck()
    {
        for(int i = 0; i < stages.Length; i++)
        {
            if(health > stages[i].stage_OnHealth) { return; }
            else
            {
                if(!stages[i].isTrigger)
                {
                    stages[i].isTrigger = true;
                    stages[i].stageEvent?.Invoke();
                    return;
                }
            }
        }
    }
    private float healthPersentage(float newHealth)
    {
        if (newHealth < 0 || newHealth > MaxHealth) return 0;

        float persenHealth = newHealth / MaxHealth;
        return persenHealth;
    }
}
