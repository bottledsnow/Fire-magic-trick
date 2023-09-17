using UnityEngine;  

public class PrototypeNALL : MonoBehaviour
{
    [SerializeField] private GameObject Magazing_UI;
    [SerializeField] private GameObject ShootingMode_UI;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        Magazing_UI.SetActive(false);
        ShootingMode_UI.SetActive(false);
    }
}
