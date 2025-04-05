using UnityEngine;

public static class UpgradeSystem
{
    public static void ApplyUpgrade(PlayerHealth player, UpgradeType upgradeType, float amount)
    {
        var playerAttack = player.GetComponent<PlayerAttack>();
        switch (upgradeType)
        {
            case UpgradeType.MaxHealth:
                player.IncreaseMaxHealth(amount);
                break;
            case UpgradeType.ShieldCapacity:
                player.IncreaseShieldCapacity(amount);
                break;
            case UpgradeType.ShieldRegen:
                player.IncreaseShieldRegenRate(amount);
                break;
            case UpgradeType.FullHeal:
                player.RegenerateFullHealth();
                break;
            case UpgradeType.MissileCapacity:
                playerAttack.AddMissileCapacity((int)amount); // Aumenta la capacidad de misiles
                break;
            case UpgradeType.MissileReload:
                playerAttack.ReloadMissiles(); // Recarga todos los misiles
                break;
        }
    }
}
