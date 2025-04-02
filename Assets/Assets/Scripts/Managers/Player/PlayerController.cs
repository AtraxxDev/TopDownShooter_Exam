using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();

       // playerHealth.OnPlayerDie += Die

        playerHealth.InitializeStats();

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


  

  

}
