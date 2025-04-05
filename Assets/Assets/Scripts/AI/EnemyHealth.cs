using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    private float maxHealth;
    private float currentHealth;

    public delegate void HealthChangedDelegate(float currentHealth);
    public event HealthChangedDelegate OnHealthChanged;

    public event Action OnEnemyDeath;  // Evento para la muerte del enemigo

    public void Initialize(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;

        // Notificamos la salud inicial
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnEnemyDeath?.Invoke();

        if (UnityEngine.Random.Range(0f, 1f) <= 0.2f)
        {
            CoinManager.Instance.AddCoins(3); // Agregar 5 monedas al CoinManager
            // aqui iria el sonido de añadir una moneda
        }

        gameObject.SetActive(false);
        ScoreManager.Instance.IncreaseScore();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
