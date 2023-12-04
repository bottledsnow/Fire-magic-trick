using UnityEngine;
using TMPro;

public class TeachWordSystem : MonoBehaviour
{
    [SerializeField] private Color color;
    [TextArea(3, 10)]
    [SerializeField] private string text;

    private Animator animator;
    private TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();

        InitializationWord();
    }
    private void InitializationWord()
    {
        textMeshProUGUI.color = new Color(color.r, color.g, color.b, color.a);
        textMeshProUGUI.text = text;
    }
    public void OpenTeachWord()
    {
        animator.Play("Open");
    }
    public void CloseTeachWord()
    {
        animator.Play("Close");
    }
}
