using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private StatsDataSO statsData;
    private float currentHealth;
    private float currentShield;
    private Coroutine shieldRegenCoroutine;
    private bool isTakingDamage;

    public float currentMaxHealth;
    public float currentShieldCapacity;
    public float currentShieldRegenRate;

    public event Action OnPlayerDie;

    public void InitializeStats()
    {
        // Inicializamos las variables a los valores del ScriptableObject
        currentMaxHealth = statsData.maxHealth;
        currentHealth = currentMaxHealth; // La salud inicial es igual al máximo
        currentShieldCapacity = statsData.shieldCapacity;
        currentShield = currentShieldCapacity; // El escudo también empieza con su capacidad máxima
        currentShieldRegenRate = statsData.shieldRegenRate;
    }

    public void IncreaseMaxHealth(float amount)
    {
        currentMaxHealth += amount;
        currentHealth = Mathf.Min(currentHealth, currentMaxHealth); // Aseguramos que la salud no exceda el nuevo máximo
    }

    // Método para aumentar la capacidad del escudo
    public void IncreaseShieldCapacity(float amount)
    {
        currentShieldCapacity += amount;
        currentShield = Mathf.Min(currentShield, currentShieldCapacity); // Aseguramos que el escudo no exceda la nueva capacidad
    }

    // Método para aumentar la tasa de regeneración del escudo
    public void IncreaseShieldRegenRate(float amount)
    {
        currentShieldRegenRate += amount;
    }

    // Método para regenerar toda la vida
    public void RegenerateFullHealth()
    {
        currentHealth = currentMaxHealth;
    }

    // Método para aplicar el daño
    public void TakeDamage(float damage)
    {
        isTakingDamage = true;
        if (shieldRegenCoroutine != null)
        {
            StopCoroutine(shieldRegenCoroutine); // Si estaba regenerando el escudo, se detiene
        }

        if (currentShield > 0)
        {
            float remainingDamage = damage - currentShield;
            currentShield = Mathf.Max(0, currentShield - damage);

            if (remainingDamage > 0)
            {
                currentHealth = Mathf.Max(0, currentHealth - remainingDamage);
            }
        }
        else
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Se espera un tiempo antes de comenzar la regeneración del escudo
            StartCoroutine(ShieldRegenDelay());
        }
    }

    private IEnumerator ShieldRegenDelay()
    {
        yield return new WaitForSeconds(3f); // Tiempo sin recibir daño antes de comenzar la regeneración
        isTakingDamage = false;
        shieldRegenCoroutine = StartCoroutine(RegenerateShield());
    }

    private IEnumerator RegenerateShield()
    {
        while (currentShield < currentShieldCapacity && !isTakingDamage)
        {
            currentShield = Mathf.Min(currentShieldCapacity, currentShield + currentShieldRegenRate * Time.deltaTime);
            yield return null;
        }
    }

    private void Die()
    {
        // Lógica de muerte
        OnPlayerDie?.Invoke();
    }
}
