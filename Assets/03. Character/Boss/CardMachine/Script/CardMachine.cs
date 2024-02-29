using System.Collections;
using UnityEngine;

public class CardMachine : Boss
{
    [Header("Enemy")]
    [SerializeField] private GameObject Enemy_A;
    [SerializeField] private GameObject Enemy_B;
    [SerializeField] private GameObject Enemy_C;
    [Header("Spawn Point")]
    [SerializeField] private SpawnPoint spawnPoint_entry;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    public void OnBossFight()
    {
        StartCoroutine(TimerCoroutine_Entry());
    }
    IEnumerator TimerCoroutine_Entry()
    {
        while (true)
        {
            spawnPoint_entry.Spawn();
            yield return new WaitForSeconds(7f);
        }
    }
}
