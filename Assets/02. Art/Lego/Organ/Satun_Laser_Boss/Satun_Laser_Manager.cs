using UnityEngine;

public class Satun_Laser_Manager : MonoBehaviour
{
    private Satun_Laser[] satuns;

    private void Awake()
    {
        satuns = GetComponentsInChildren<Satun_Laser>();
    }
    public void active(bool active)
    {
        for(int i=0; i<satuns.Length;i++)
        {
            satuns[i].active(active);
        }
    }
}
