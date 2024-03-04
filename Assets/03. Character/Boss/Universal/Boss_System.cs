using UnityEngine;
using UnityEngine.Events;

public class Boss_System : MonoBehaviour
{
    [Header("Setting")]
    public Boss_UI UI;
    public Barrier barrier;
    public UnityEvent OnStartFight;
    public UnityEvent OnResetFight;
    [Header("Boss")]
    [SerializeField] private string boss_name;
    [SerializeField] private string boss_littleTitle;
    [Space(10)]
    [SerializeField] private Boss boss;

    private ProgressSystem progress;

    private bool isBoss;
    private bool isWin;

    private void Start()
    {
        progress = GameManager.singleton.GetComponent<ProgressSystem>();

        progress.OnPlayerDeath += ResetBoss;
    }
    public void ResetBoss()
    {
        if (isBoss)
        {
            isBoss = false;

            UI.Boss_Exit();
            barrier.Close();
            boss.ResetBossFight();
            OnResetFight?.Invoke();
            Debug.Log("Boss Fight Reset");
        }
    }
    public void StartBossFight()
    {
        if (isWin) return;
        if (!isBoss) isBoss = true;
        UI.Boss_Enter(boss_name, boss_littleTitle);
        barrier.Open();
        OnStartFight?.Invoke();
    }
    public void EndBossFight()
    {
        if(isBoss)
        {
            isBoss = false;

            UI.Boss_Exit();
            barrier.Close();
            boss.gameObject.SetActive(false);
        }
    }
    public void SetHealth(float newHealthpersen)
    {
        UI.SetValue(newHealthpersen);
    }
    public void DebugTest(string word)
    {
        Debug.Log(word);
    }
}
