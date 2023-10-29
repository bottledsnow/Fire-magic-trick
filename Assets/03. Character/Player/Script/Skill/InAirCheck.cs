using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirCheck : MonoBehaviour
{
    [SerializeField] private float AirLengh;
    public bool InAir;

    private void Update()
    {
        Raycheck();
    }

    private void Raycheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, AirLengh))
        {
            Debug.Log("射?命中了物體：" + hit.collider.gameObject.name);
            InAir = false;
        }else
        {
            InAir = true;
        }
    }
    private void OnDrawGizmos()
    {
        // 設置 Gizmos 的?色
        Gizmos.color = Color.yellow;

        // 繪製射?，起點為物體位置，方向為向下
        Gizmos.DrawRay(transform.position, Vector3.down * AirLengh);
    }
}
