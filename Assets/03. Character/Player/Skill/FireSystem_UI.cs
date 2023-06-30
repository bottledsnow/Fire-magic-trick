using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSystem_UI : MonoBehaviour
{
    private EnergySystem energySystem;
    [Header("Energy")]
    [SerializeField ]private Image fireEnergy;
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
    }

    private void EnergyColor()
    {
        fireEnergy.color = Color.Lerp(NullEnergy, FullEnergy, energySystem.FireEnergy / 100);
    }
    private void EnergyBar()
    {
        fireEnergy.fillAmount = energySystem.FireEnergy / 100;
    }
}
