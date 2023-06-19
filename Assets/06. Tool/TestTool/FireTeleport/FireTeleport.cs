using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTeleport : MonoBehaviour
{
    private Transform Bullet;
    private Transform spawnBulletPosition;

    private void Update()
    {
        FireSkillBullet();
    }

    private void FireSkillBullet()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            instantiateBullet();
        }
    }

    private void instantiateBullet()
    {
        Instantiate(Bullet, spawnBulletPosition.position,Quaternion.identity);
    }
}
