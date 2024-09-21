using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditCardLink : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string url;
    [SerializeField] private Image image;

    [SerializeField] private float hoverAlpha = 0.85f;
    private float defaultAlpha;

    private bool isHovering = false;

    private void OnEnable()
    {
        isHovering = false;
        defaultAlpha = image.color.a;
    }

    private void OnDisable()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, defaultAlpha);
    }

    private void Update()
    {
        if (isHovering)
        {
            float targetAlpha = Mathf.Lerp(image.color.a, hoverAlpha, Time.deltaTime * 10f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
        }
        else
        {
            float targetAlpha = Mathf.Lerp(image.color.a, defaultAlpha, Time.deltaTime * 10f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
