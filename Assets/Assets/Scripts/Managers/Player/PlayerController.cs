using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    PlayerAnimations playerAnimations;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAnimations = GetComponent<PlayerAnimations>();

        playerHealth.OnPlayerDie += PlayerDie;

        playerHealth.InitializeStats();


    }

    private void Update()
    {
        playerMovement.RotateTowardsMouse(inputHandler.MousePosition);

        if (inputHandler.isFiring)
        {
            playerAttack.Attack();
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot);
        }
        else
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot,false);

        }

    }
    private void FixedUpdate()
    {
        playerMovement.Move(inputHandler.MoveInput);
        playerAnimations.PlayAnimation(PlayerAnimationState.Walk);

        if (inputHandler.MoveInput == Vector2.zero)
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Walk,false);

        }


    }

    public void PlayerDie()
    {
        StartCoroutine(HandlePlayerDie());
    }
    public IEnumerator HandlePlayerDie()
    {
        // Reproducir la animación de muerte
        playerAnimations.PlayAnimation(PlayerAnimationState.Die);

        // Esperar hasta que la animación de muerte haya terminado
        AnimatorStateInfo stateInfo = playerAnimations.animator.GetCurrentAnimatorStateInfo(0);

        // Esperar hasta que la animación termine
        yield return new WaitForSeconds(stateInfo.length);

        // Después de que la animación de muerte termine, puedes hacer lo que sea necesario (ejemplo: desactivar al jugador)
        gameObject.SetActive(false); // O cualquier otro comportamiento que quieras implementar
    }

  

  

}
