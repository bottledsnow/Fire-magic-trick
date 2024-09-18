using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditUI : MainMenuUIBase
{
    [SerializeField] private MainMenu mainMenu;

    public void OnBackClick()
    {
        Deactivate();
        mainMenu.Activate();
    }
}
