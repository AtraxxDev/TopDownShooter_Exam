using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image shieldBarFill;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateUI;
        playerHealth.OnShieldChanged += UpdateUI;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateUI;
        playerHealth.OnShieldChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        healthBarFill.fillAmount = playerHealth.GetHealthNormalized();
        shieldBarFill.fillAmount = playerHealth.GetShieldNormalized();
    }
}
