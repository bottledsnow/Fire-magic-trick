using UnityEngine;

public class DeathArea : MonoBehaviour
{
    private DeathSystem _deathSystem;

    private void Start()
    {
        _deathSystem = GameManager.singleton.UISystem.GetComponent<DeathSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _deathSystem.EnterDeathImage();
        }
        if(other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
