using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

public class Boss_System : MonoBehaviour
{
    [Header("Setting")]
    public Barrier barrier;
    [SerializeField] private MMF_Player reserFeedback;
    [Header("Boss")]
    [SerializeField] private string boss_name;
    [SerializeField] private string boss_littleTitle;
    [SerializeField] private string bossBgmName;
    [SerializeField] private string normalBgmName;

    public delegate void OnStartFightHandler();
    public event OnStartFightHandler onStartFight;
    public delegate void OnResetFightHandler();
    public event OnResetFightHandler onResetFight;
    public delegate void OnEndFightHandler();
    public event OnEndFightHandler onEndFight;

    private bool isBoss;

    private void Start()
    {
        GameManager.Instance.OnPlayerReborn += ResetBoss;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerReborn -= ResetBoss;
    }
    private void Update()
    {
        if(isBoss)
        {
            if (this.gameObject.activeSelf == false) 
            {
                this.gameObject.SetActive(true);
            }
        }
    }
    public void ResetBoss()
    {
        if (isBoss)
        {
            isBoss = false;

            UIManager.Instance.HudUI.CloseBossUI();
            AudioManager.Instance.PlayBGM(normalBgmName);
            barrier.Close();
            reserFeedback.PlayFeedbacks();
            onResetFight?.Invoke();
        }
    }
    public void StartBossFight()
    {
        if (!isBoss)
        {
            isBoss = true;

            UIManager.Instance.HudUI.OpenBossUI(boss_name, boss_littleTitle);
            barrier.Open();
            reserFeedback.PlayFeedbacks();
            onStartFight?.Invoke();

            AudioManager.Instance.PlayBGM(bossBgmName);
        }
    }
    public void EndBossFight()
    {
        if(isBoss)
        {
            isBoss = false;

            onEndFight?.Invoke();
            UIManager.Instance.HudUI.CloseBossUI();
            DataPersistenceManager.Instance.SaveGame();
            barrier.Close();
        }
    }
    public void SetHealth(float newHealthpersen)
    {
        UIManager.Instance.HudUI.SetBossHealth(newHealthpersen);
    }
    public void SetIsWind(bool active)
    {
        isBoss = active;
    }
    public void DebugTest(string word)
    {
        Debug.Log(word);
    }
}
