using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShootingSystem_UI : MonoBehaviour
{
    
    private EnergySystem energySystem;
    [Header("Magazing")]
    [SerializeField] private Shooting_Magazing _magazing;
    [SerializeField] private Image Magazing;
    [SerializeField] private float TestNumber;
    [Header("Energy")]
    [SerializeField] private Image ShootingEnergy;
    [SerializeField] private Color FullEnergy;
    [SerializeField] private Color NullEnergy;
    private void Start()
    {
        energySystem = GameManager.singleton._input.gameObject.GetComponent<EnergySystem>();
    }

    private void Update()
    {
        EnergyBar();
        EnergyColor();
        MagazingBar();
    }
    #region old System 
    private void EnergyColor()
    {
        ShootingEnergy.color = Color.Lerp(NullEnergy, FullEnergy, energySystem.ShootingEnergy / 100);
    }
    private void EnergyBar()
    {
        ShootingEnergy.fillAmount = energySystem.ShootingEnergy / 100;
    }
    #endregion
    #region new System
    private void MagazingBar()
    {
        Magazing.fillAmount = TestNumber;
    }
    #endregion
}
