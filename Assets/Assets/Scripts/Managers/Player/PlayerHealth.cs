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
    public event Action OnHealthChanged;
    public event Action OnShieldChanged;

    private void Awake()
    {
        InitializeStats();
    }

    public void InitializeStats()
    {
        currentMaxHealth = statsData.maxHealth;
        currentHealth = currentMaxHealth;
        currentShieldCapacity = statsData.shieldCapacity;
        currentShield = currentShieldCapacity;
        currentShieldRegenRate = statsData.shieldRegenRate;

        OnHealthChanged?.Invoke();
        OnShieldChanged?.Invoke();
    }

    public void IncreaseMaxHealth(float amount)
    {
        currentMaxHealth += amount;
        currentHealth = Mathf.Min(currentHealth, currentMaxHealth);
        OnHealthChanged?.Invoke();
    }

    public void IncreaseShieldCapacity(float amount)
    {
        currentShieldCapacity += amount;
        currentShield = Mathf.Min(currentShield, currentShieldCapacity);
        OnShieldChanged?.Invoke();
    }

    public void IncreaseShieldRegenRate(float amount)
    {
        currentShieldRegenRate += amount;
    }

    public void RegenerateFullHealth()
    {
        currentHealth = currentMaxHealth;
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        isTakingDamage = true;
        if (shieldRegenCoroutine != null)
        {
            StopCoroutine(shieldRegenCoroutine);
        }

        if (currentShield > 0)
        {
            float remainingDamage = damage - currentShield;
            currentShield = Mathf.Max(0, currentShield - damage);
            OnShieldChanged?.Invoke();

            if (remainingDamage > 0)
            {
                currentHealth = Mathf.Max(0, currentHealth - remainingDamage);
                OnHealthChanged?.Invoke();
            }
        }
        else
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            OnHealthChanged?.Invoke();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ShieldRegenDelay());
        }
    }

    private IEnumerator ShieldRegenDelay()
    {
        yield return new WaitForSeconds(3f);
        isTakingDamage = false;
        shieldRegenCoroutine = StartCoroutine(RegenerateShield());
    }

    private IEnumerator RegenerateShield()
    {
        while (currentShield < currentShieldCapacity && !isTakingDamage)
        {
            currentShield = Mathf.Min(currentShieldCapacity, currentShield + currentShieldRegenRate * Time.deltaTime);
            OnShieldChanged?.Invoke();
            yield return null;
        }
    }

    private void Die()
    {
        OnPlayerDie?.Invoke();
        gameObject.SetActive(false);
    }

    public float GetHealthNormalized() => currentHealth / currentMaxHealth;
    public float GetShieldNormalized() => currentShield / currentShieldCapacity;
}
