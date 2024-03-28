using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private GameObject EnegyReturn;
    [SerializeField] float eachTime;

    private float timer;
    private bool isActive;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SetIsActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(isActive)
        {
            SetIsActive(false);
            timer = 0;
        }
    }
    private void Update()
    {
        timerSystem();
    }
    private void timerSystem()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
        }

        if(timer > eachTime)
        {
            timer = 0;
            SpawnEnegy();
        }
    }
    private void SpawnEnegy()
    {
        GameObject obj = Instantiate(EnegyReturn,this.transform.position,Quaternion.identity);
        Destroy(obj,2.5f);
    }
    private void SetIsActive(bool active)
    {
        isActive = active;
    }
}
