using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamagable, IHealth
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false); // O cambiar animación de muerte
    }
}
