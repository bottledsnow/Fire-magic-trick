using UnityEngine;
[System.Serializable]
public struct BoolParir
{
    public bool bool1;
    public bool bool2;
}
public class PumberManager : MonoBehaviour
{
    public Pumbers[] pumbers;
    public BoolParir boolPair1;
    public BoolParir boolPair2;
    public void CloseAllPumbers()
    {
        for(int i=0;i<pumbers.Length;i++)
        {
            pumbers[i].SetPumbersState(0);
        }
    }
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
