using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    public void PlayerChoosePoint()
    {
        if (firePoint != null)
            firePoint.SetActive(true);
    }
    public void PlayerNotChoosePoint()
    {
        Debug.Log("Leave Point");
        if (firePoint != null)
            firePoint.SetActive(false);
    }
    public void DestroyChoosePoint()
    {
        Destroy(this.gameObject);
    }
}
