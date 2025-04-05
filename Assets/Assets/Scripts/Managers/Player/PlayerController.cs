using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private PlayerAnimations playerAnimations;

    public bool isOpenShop;

    private bool isWalking = false;
    private float walkSFXCooldown = 0.5f; // Tiempo entre cada repetición del sonido
    private float walkSFXTimer = 0f; // Temporizador que lleva el conteo
    private float shootSFXCooldown = 0.1f; // Tiempo de espera entre disparos para el SFX
    private float shootSFXTimer = 0f; // Temporizador para el sonido de disparo

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

        if (inputHandler.isFiring && !isOpenShop)
        {
            playerAttack.Attack();  // Realiza el ataque
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot); // Reproduce la animación de disparo

            // Verificamos si el temporizador de disparo ha llegado a 0
            shootSFXTimer -= Time.deltaTime;

            // Solo reproducimos el SFX si ha pasado el tiempo de cooldown
            if (shootSFXTimer <= 0f)
            {
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Shoot);
                shootSFXTimer = shootSFXCooldown; // Reiniciamos el temporizador
            }
        }
        else
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot, false);
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(inputHandler.MoveInput);

        // Actualizamos el temporizador de caminar
        walkSFXTimer -= Time.deltaTime;

        if (inputHandler.MoveInput != Vector2.zero)
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Walk);

            if (walkSFXTimer <= 0f)
            {
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Walk);
                walkSFXTimer = walkSFXCooldown;
            }
        }
        else
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Walk, false);
            AudioManager.Instance.StopSFX(AudioManager.SFXType.Walk);
        }
    }

    public void PlayerDie()
    {
        StartCoroutine(HandlePlayerDie());
    }

    public IEnumerator HandlePlayerDie()
    {
        playerAnimations.PlayAnimation(PlayerAnimationState.Die);
        AnimatorStateInfo stateInfo = playerAnimations.animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        gameObject.SetActive(false);
    }
}
