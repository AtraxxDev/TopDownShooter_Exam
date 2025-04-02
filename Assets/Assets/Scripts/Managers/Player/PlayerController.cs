using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();

        currentHealth = maxHealth;

    }

    private void Update()
    {
        playerMovement.RotateTowardsMouse(inputHandler.MousePosition);

        if (inputHandler.isFiring)
        {
            playerAttack.Attack();
        }

    }
    private void FixedUpdate()
    {
        playerMovement.Move(inputHandler.MoveInput);

       
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
