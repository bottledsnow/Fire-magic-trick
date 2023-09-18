using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeN7 : MonoBehaviour
{
    [SerializeField] private Shooting_Magazing _shootingMagazing;

    public void ClearMagazing()
    {
        _shootingMagazing.ClearBullet();
    }
}
