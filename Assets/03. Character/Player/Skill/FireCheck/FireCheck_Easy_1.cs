using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCheck : MonoBehaviour
{
    public float distance = 10f; // 區域距離相機的距離
    public float angle = 180f; // 半圓形的角度
    public float size = 5f; // 區域的大小
    public LayerMask detectionMask; // 檢測物件的LayerMask

    private List<Renderer> highlightedRenderers = new List<Renderer>();

    private void Update()
    {
        // 取得相機在世界空間中的位置和正前方方向
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 cameraForward = Camera.main.transform.forward;

        // 計算半圓形區域的中心位置
        Vector3 center = cameraPosition + cameraForward * distance;

        // 只檢測區域內的物件
        Collider[] colliders = Physics.OverlapSphere(center, size, detectionMask);

        // 儲存當前被突出顯示的物件
        List<Renderer> currentHighlightedRenderers = new List<Renderer>();

        // 檢查每個檢測到的物件
        foreach (Collider collider in colliders)
        {
            // 將物件的位置轉換為相機空間中的座標
            Vector3 colliderPosition = Camera.main.WorldToViewportPoint(collider.transform.position);

            // 檢查座標是否在半圓形範圍內
            if (colliderPosition.y <= 0.5f && colliderPosition.x >= 0.25f && colliderPosition.x <= 0.75f)
            {
                // 檢查標籤是否為 "FirePoint"
                if (collider.CompareTag("FirePoint"))
                {
                    Debug.Log("FirePoint detected!");

                    // 獲取物件的 Renderer 組件
                    Renderer renderer = collider.GetComponent<Renderer>();

                    // 檢查是否存在 Renderer 組件並且有可設置的材質
                    if (renderer != null && renderer.material != null)
                    {
                        // 將材質顏色設置為紅色
                        renderer.material.color = Color.red;

                        // 將被突出顯示的物件加入列表中
                        currentHighlightedRenderers.Add(renderer);
                    }
                }
            }
        }

        // 將離開檢測範圍的物件恢復為白色
        foreach (Renderer highlightedRenderer in highlightedRenderers)
        {
            if (!currentHighlightedRenderers.Contains(highlightedRenderer))
            {
                // 檢查是否存在 Renderer 組件並且有可設置的材質
                if (highlightedRenderer != null && highlightedRenderer.material != null)
                {
                    // 將材質顏色設置為白色
                    highlightedRenderer.material.color = Color.white;
                }
            }
        }

        // 更新當前被突出顯示的物件列表
        highlightedRenderers = currentHighlightedRenderers;
    }
}
