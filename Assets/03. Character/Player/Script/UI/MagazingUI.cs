using UnityEngine;

public class MagazingUI : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;

    private void Initialization()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }
    public void UpdateBulletsNumber(int BulletNumber)
    {
        Initialization();
        for (int i = 0; i <= BulletNumber; i++)
        {
            bullets[i].SetActive(true);
        }
    }
}
