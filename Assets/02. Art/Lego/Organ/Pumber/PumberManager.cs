using System.Collections;
using UnityEngine;
[System.Serializable]
public class ArrayLayout
{
    [System.Serializable]
    public struct rowData
    {
        public bool[] row;
    }
    public rowData[] rows = new rowData[9]; 
}
[System.Serializable]
public class ArrayPumbers
{
    [System.Serializable]
    public struct rowData
    {
        public Pumber[] row;
    }
    public rowData[] rows = new rowData[9];
}
public class PumberManager : MonoBehaviour
{
    public Pumbers[] pumbers;
    public ArrayPumbers pumbersLayout;
    [Header("Event A")]
    public int state_140;
    public ArrayLayout Event_140;
    [Header("Event B")]
    public int state_110;
    public ArrayLayout Event_110;
    [Header("Event C")]
    public int state_70;
    public ArrayLayout Event_70;
    [Header("Event D")]
    public int state_30;
    public ArrayLayout Event_30;
    [Header("Bounce")]
    public int Bounce_force;
    public float Bounce_duration;
    public float Bounce_duration_Ramdon;

    private ImpactReceiver impactReceiver;
    private void Start()
    {
        if(Bounce_duration_Ramdon==0) Bounce_duration_Ramdon = 0.25f;
        if (Bounce_duration == 0) Bounce_duration = 5;

        
    }
    public void BossFight_PumbersEvent(ArrayLayout eventlayout,int state)
    {
        for (int i = 0; i < pumbersLayout.rows.Length; i++)
        {
            for (int j = 0; j < pumbersLayout.rows[i].row.Length; j++)
            {
                if (eventlayout.rows[i].row[j] == true)
                {
                    pumbersLayout.rows[i].row[j].SetAnimatorState(state);
                }
            }
        }
    }
    IEnumerator BounceDuration(int i,int j)
    {
        while (true)
        {
            pumbersLayout.rows[i].row[j].SuperBounce();
            yield return new WaitForSeconds(Bounce_duration);
        }
    }
    IEnumerator RamdomBounce()
    {
        int i;
        int j;

        while (true)
        {
            i = Random.Range(0, pumbersLayout.rows.Length);
            j = Random.Range(0, pumbersLayout.rows[i].row.Length);

            pumbersLayout.rows[i].row[j].SuperBounce();
            yield return new WaitForSeconds(Bounce_duration_Ramdon);
        }
    }
    public void Boss_140()
    {
        BossFight_PumbersEvent(Event_140, state_140);
    }
    public void Boss_110()
    {
        BossFight_PumbersEvent(Event_140, state_140 - 1);
        BossFight_PumbersEvent(Event_110,state_110);
    }
    public void Boss_70()
    {
        BossFight_PumbersEvent(Event_70, state_70);

        StartCoroutine(BounceDuration(1, 1));
        StartCoroutine(BounceDuration(1, 7));
        StartCoroutine(BounceDuration(7, 1));
        StartCoroutine(BounceDuration(7, 7));
    }
    public void Boss_30()
    {
        CloseAllPumbers();
        StartCoroutine(RamdomBounce());
        StartCoroutine(RamdomBounce());
        StartCoroutine(RamdomBounce());
        StartCoroutine(RamdomBounce());
        StartCoroutine(RamdomBounce());
    }
    public void CloseAllPumbers()
    {
        Debug.Log("Close");
        for(int i=0;i<pumbers.Length;i++)
        {
            pumbers[i].SetPumbersState(0);
        }
        StopAllCoroutines();
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
