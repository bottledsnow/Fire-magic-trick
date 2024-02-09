using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemys;

    //Script
    private ProgressSystem progressSystem;

    //event
    public event MyDelegates.OnHandler OnSpawn;
    public event MyDelegates.OnHandler OnDeath;
    public event MyDelegates.OnHandler OnClear;

    //variable
    public bool isClear;
    public bool isSpawned;

    private void Start()
    {
        //Script
        progressSystem = GameManager.singleton.GetComponent<ProgressSystem>();

        //event
        progressSystem.OnPlayerDeath += ToDeath;

    }
    private void Update()
    {
        if(!isClear)
        {
            clearCheck();
        }
    }
    public void ToSpawn()
    {
        if(!isClear)
        {
            OnSpawn?.Invoke();
            setIsSpawned(true);
        }
    }
    public void ToDeath()
    {
        if(!isClear)
        {
            OnDeath?.Invoke();
            setIsSpawned(false);
        }
    }
    public void ToClear()
    {
        OnClear?.Invoke();
        setIsClear(true);
    }
    private void clearCheck()
    {
        for(int i = 0; i < Enemys.Length; i++)
        {
            if (Enemys[i] != null)
            {
                if(Enemys[i].activeSelf == true)
                {
                    return;
                }
            }
        }
        ToClear();
    }
    private void setIsClear(bool value)
    {
        isClear = value;
    }
    private void setIsSpawned(bool value)
    {
        isSpawned = value;
    }
}
