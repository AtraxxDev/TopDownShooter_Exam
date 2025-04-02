using UnityEngine;

public class AIEnemy : MonoBehaviour,IDamagable
{
    //[SerializeField] private StatsDataSO statsDataSO;

    private float maxHealth = 100;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

   
}
