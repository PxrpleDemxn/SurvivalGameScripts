using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerData _playerAttributes;

    [SerializeField] protected Image Food;
    [SerializeField] protected Image Water;
    [SerializeField] protected TextMeshProUGUI Energy;
    [SerializeField] protected Image Health;
    [SerializeField] protected TextMeshProUGUI Stamina;

    // private void OnEnable()
    // {
    //     _playerAttributes.OnFoodChanged += UpdateFoodUI;
    //     _playerAttributes.OnWaterChanged += UpdateWaterUI;
    //     _playerAttributes.OnEnergyChanged += UpdateEnergyUI;
    //     _playerAttributes.OnHealthChanged += UpdateHealthUI;
    //     _playerAttributes.OnStaminaChanged += UpdateStaminaUI;
    // }
    //
    // private void OnDisable()
    // {
    //     _playerAttributes.OnFoodChanged -= UpdateFoodUI;
    //     _playerAttributes.OnWaterChanged -= UpdateWaterUI;
    //     _playerAttributes.OnEnergyChanged -= UpdateEnergyUI;
    //     _playerAttributes.OnHealthChanged -= UpdateHealthUI;
    //     _playerAttributes.OnStaminaChanged -= UpdateStaminaUI;
    // }

    private void UpdateFoodUI(int currentFood, int maxFood)
    {
        Food.fillAmount = (float)currentFood / maxFood;
    }
    private void UpdateWaterUI(int currentWater, int maxWater)
    {
        Water.fillAmount = (float)currentWater / maxWater;
    }
    private void UpdateEnergyUI(int energy, int maxEnergy)
    {
        //Energy.text = $"Energy: {energy}/{maxEnergy}";
        //TODO Add energy UI update logic
    }
    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        Health.fillAmount = currentHealth / maxHealth;
    }
    private void UpdateStaminaUI(float currentStamina, float maxStamina)
    {
        Stamina.text = $"Stamina: {currentStamina}/{maxStamina}";
    }
}
