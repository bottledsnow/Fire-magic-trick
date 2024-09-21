using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Deactivate should always be called first before Activate any other UI.
/// </summary>
public class MouseControlUIBase : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectObj;
    public virtual void Activate()
    {
        gameObject.SetActive(true);
        PlayerInputHandler.Instance.ResetAllInput();
        EventSystem.current.SetSelectedGameObject(firstSelectObj);
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
