using UnityEngine;

public class EventManager_GlassBox_2D : MonoBehaviour
{
    private Event_GlassBox_2D[] boxs;
    private void OnValidate()
    {
        boxs = GetComponentsInChildren<Event_GlassBox_2D>();
    }
    public void Broke()
    {
        for (int i = 0; i < boxs.Length; i++)
        {
            boxs[i].Broken();
        }
    }
}
