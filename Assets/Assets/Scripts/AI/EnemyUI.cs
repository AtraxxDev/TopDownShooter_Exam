using UnityEngine;
using UnityEngine.UI;  

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;   
    [SerializeField] private Image healthBar;         

    private void OnEnable()
    {
        enemyHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        // Calculamos el fillAmount para la barra de salud basado en la salud actual
        float fillAmount = currentHealth / enemyHealth.GetMaxHealth();
        healthBar.fillAmount = fillAmount;  // Actualiza la imagen (barra de salud)
    }
}
