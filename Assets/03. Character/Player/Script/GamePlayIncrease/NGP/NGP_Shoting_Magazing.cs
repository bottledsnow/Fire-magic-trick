using UnityEngine;

public class NGP_Shoting_Magazing : Shooting_Magazing
{
    protected override bool CanReload()
    {
        //energySystem.UseReload(out CanUse);
        return true;
    }
}
