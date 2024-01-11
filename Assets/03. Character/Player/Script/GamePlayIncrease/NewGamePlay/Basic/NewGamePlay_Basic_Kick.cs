using UnityEngine;

public class NewGamePlay_Basic_Kick : MonoBehaviour
{
    //Script
    private SuperDashKick kick;
    private NewGamePlay_Combo combo;

    protected virtual void Start()
    {
        kick =GameManager.singleton.EnergySystem.GetComponent<SuperDashKick>();
        combo = GetComponent<NewGamePlay_Combo>();

        //Subscribe
        kick.OnKick += Onkick;
    }
    protected virtual void Onkick() { combo.SetComboShotType(NewGamePlay_Combo.ComboShotType.FireCard); }
}
