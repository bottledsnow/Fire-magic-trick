using UnityEngine;

public class PumberManager : MonoBehaviour
{
    public Pumbers[] pumbers;

    public void SetPumbersState(int id,int state)
    {
        pumbers[id].SetPumbersState(state);
    }
    public void BossFight_A()
    {
        SetPumbersState(0, 1);
    }
    public void BossFight_B() 
    {
        SetPumbersState(1, 2);
    }
    public void BossFight_C()
    {
        SetPumbersState(2, 3);
    }
}
