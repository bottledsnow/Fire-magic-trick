using UnityEngine;

public class ShotTarget : MonoBehaviour
{
    void Update()
    {
        if(this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
    }
}
