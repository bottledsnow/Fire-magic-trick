using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour
{
    public Image health;
    public TextMeshProUGUI boss_name;
    public TextMeshProUGUI boss_littleTitle;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Boss_Enter(string boss_name,string littleTitle)
    {
        this.boss_name.text = boss_name;
        this.boss_littleTitle.text = littleTitle;
        animator.SetTrigger("Enter");
    }
    public void Boss_Exit()
    {
        animator.SetTrigger("Exit");
    }
    public void SetValue(float value)
    {
        health.fillAmount = value;
    }
}
