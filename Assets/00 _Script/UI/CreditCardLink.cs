using UnityEngine;
using UnityEngine.EventSystems;

public class CreditCardLink : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string url;
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(url);
    }
}
