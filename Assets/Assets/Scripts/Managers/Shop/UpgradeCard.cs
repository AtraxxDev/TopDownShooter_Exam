using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private int cost;
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private float upgradeAmount;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text descriptionText;


    private PlayerHealth playerHealth;
    private PlayerAnimations playerAnimations;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerAnimations = FindFirstObjectByType<PlayerAnimations>();
        upgradeButton.onClick.AddListener(BuyUpgrade);
        CoinManager.Instance.OnCoinsUpdated += UpdateUI; // Suscribirse al evento de monedas

    }

    private void OnDestroy()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinsUpdated -= UpdateUI; // Desuscribirse para evitar errores
        }
    }

    public void UpdateUI(int _ = 0)
    {
        costText.text = $"{cost} Coins";
        descriptionText.text = GetUpgradeDescription();
        upgradeButton.interactable = CoinManager.Instance.HasEnoughCoins(cost);
    }

    private string GetUpgradeDescription()
    {
        switch (upgradeType)
        {
            case UpgradeType.MaxHealth: return $"Aumenta la vida máxima en: {upgradeAmount}";
            case UpgradeType.ShieldCapacity: return $"Aumenta la cantidad máxima de escudo en: {upgradeAmount}";
            case UpgradeType.ShieldRegen: return $"Aumenta la velocidad de regeneración del escudo en: {upgradeAmount}";
            case UpgradeType.FullHeal: return "Regenera toda la vida";
            default: return "Unknown Upgrade";
        }
    }


    private void BuyUpgrade()
    {
        if (!CoinManager.Instance.HasEnoughCoins(cost)) return;

        CoinManager.Instance.SpendCoins(cost);
        UpgradeSystem.ApplyUpgrade(playerHealth, upgradeType, upgradeAmount);
        UpdateUI();
        playerAnimations.PlayAnimation(PlayerAnimationState.Jump);
    }
}

public enum UpgradeType
{
    MaxHealth,
    ShieldCapacity,
    ShieldRegen,
    FullHeal
}
