using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public float Energy;
    private EnergySystemUI _energySystemUI;
    private float TestValue = 10;
    private void Start()
    {
        _energySystemUI = GameManager.singleton.UISystem.GetComponent<EnergySystemUI>();
    }
    private void Update()
    {
        Test();
    }
    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Increase(TestValue);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Decrease(TestValue);
        }
    }
    public void Increase(float energy)
    {
        Energy += energy;
        UpdateUI(Energy);
    }
    public void Decrease(float energy)
    {
        Energy -= energy;
        UpdateUI(Energy);
    }
    private void UpdateUI(float Value )
    {
        float value = Value / 100;
        _energySystemUI.UpdateBar(value);
    }
}
