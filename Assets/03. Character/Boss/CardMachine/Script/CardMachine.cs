using System.Collections;
using UnityEngine;

public class CardMachine : Boss
{
    [Header("Enemy")]
    [SerializeField] private GameObject Enemy_A;
    [SerializeField] private float spawnDelay_A;
    [Space(10)]
    [SerializeField] private GameObject Enemy_B;
    [SerializeField] private float spawnDelay_B;
    [Space(10)]
    [SerializeField] private GameObject Enemy_C;
    [SerializeField] private float spawnDelay_C;
    [Header("Spawn Point")]
    [SerializeField] private SpawnPoint spawnPoint_entry;
    [SerializeField] private SpawnPoint[] spawnPoint_sky;


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
        StartCoroutine(TimerCoroutine_Sky());
    }
    IEnumerator TimerCoroutine_Entry()
    {
        while (true)
        {
            spawnPoint_entry.Spawn();
            yield return new WaitForSeconds(spawnDelay_C);
        }
    }
    IEnumerator TimerCoroutine_Sky()
    {
        while(true)
        {
            int min = 0;
            int max = spawnPoint_sky.Length-1;
            int index = Random.Range(min, max);
            spawnPoint_sky[index].Spawn();
            yield return new WaitForSeconds(spawnDelay_B);
        }
    }
}