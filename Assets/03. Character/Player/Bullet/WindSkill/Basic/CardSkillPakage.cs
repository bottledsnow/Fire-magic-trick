using UnityEngine;

public class CardSkillPakage : MonoBehaviour
{
    [SerializeField] private bool needToNullFather = true;
    private GameObject[] bullets;

    private void Start()
    {
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        if(needToNullFather)
        {
            NullFather();
        }
    }
    private void Update()
    {
        CheckBullesEmpty();
    }
    private void NullFather()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i] != null)
            {
                bullets[i].transform.parent = null;
            }
        }
    }
    private void CheckBullesEmpty()
    {
        if(bullets != null && bullets.Length > 0)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i] != null)
                {
                    return;
                }
            }
            Destroy(gameObject);
        }
    }
}
