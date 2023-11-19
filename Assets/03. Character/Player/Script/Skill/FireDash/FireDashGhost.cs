using UnityEngine;
using System.Threading.Tasks;

public class FireDashGhost : MonoBehaviour
{
    [SerializeField] private bool useGhost;
    [Header("Ghost")]
    [SerializeField] private int GhostInterval_ms=20;
    [SerializeField] private int GhostIntervalDissapear_ms = 50;
    [Header("Ghost Preferb")]
    [SerializeField] private Transform[] pre_Ghost;

    private GameObject[] Ghost = new GameObject[6];
    private bool firstGhost;
    public void GhostPlay()
    {
        if(useGhost)
        {
            if (!firstGhost)
            {
                firstGhostPlay();
            }
            else
            {
                ghostPlay();
            }
        }
    }
    public void GhostStop()
    {
        if(useGhost)
        {
            ghostStop();
        }
    }
    private async void firstGhostPlay()
    {
        for (int i = 0; i < 6; i++)
        {
            Ghost[i] = InstantiateGhostObj(pre_Ghost[i]);
            Ghost[i].SetActive(true);
            await Task.Delay(GhostInterval_ms);
        }
    }
    private async void ghostPlay()
    {
        for (int i = 0; i < 6; i++)
        {
            UpdatePositionEach(Ghost[i]);
            Ghost[i].SetActive(true);
            await Task.Delay(GhostInterval_ms);
        }
    }
    private async void ghostStop()
    {
        for(int i = 0; i < 6; i++)
        {
            Ghost[i].SetActive(false);
            await Task.Delay(GhostIntervalDissapear_ms);
        }
    }
    
    private GameObject InstantiateGhostObj(Transform preferb)
    {
        Transform ghost = Instantiate(preferb, transform.position, transform.rotation);
        GameObject gameObject = ghost.gameObject;
        return gameObject;
    }
    private void UpdatePositionEach(GameObject Ghost)
    {
        Ghost.transform.position = transform.position;
        Ghost.transform.rotation = transform.rotation;
    }
}
