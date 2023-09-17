using UnityEngine;

public class PrototypeNALL : MonoBehaviour
{
    [SerializeField] private GameObject Magazing_UI;
    [SerializeField] private GameObject ShootingMode_UI;
    [SerializeField] private GameObject EnergySystem_UI;
    [SerializeField] private EnergySystem EnegrySystem;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        Magazing_UI.SetActive(false);
        ShootingMode_UI.SetActive(false);
        EnergySystem_UI.SetActive(false);
        EnegrySystem.enabled = false;
    }
    public void ShowEnergy()
    {
        EnergySystem_UI.SetActive(true);
        EnegrySystem.enabled = true;
        EnegrySystem.FullEnergy();
    }
}
