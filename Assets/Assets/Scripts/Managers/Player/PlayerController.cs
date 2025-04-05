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
    private float walkSFXCooldown = 0.5f;
    private float walkSFXTimer = 0f;
    private float shootSFXCooldown = 0.1f;
    private float shootSFXTimer = 0f;

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

        // Disparo de balas normales
        if (inputHandler.isFiring && !isOpenShop)
        {
            playerAttack.Attack();
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot);

            shootSFXTimer -= Time.deltaTime;

            if (shootSFXTimer <= 0f)
            {
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Shoot);
                shootSFXTimer = shootSFXCooldown;
            }
        }
        else
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot, false);
        }

        // Disparo de misiles
        if (inputHandler.isFiringMissile && !isOpenShop)
        {
            playerAttack.AttackMissile();
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot);

            // Reproducir sonido de disparo de misil si es necesario (si no lo haces con AudioManager ya)
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(inputHandler.MoveInput);

        if (inputHandler.MoveInput != Vector2.zero)
        {
            walkSFXTimer -= Time.deltaTime;
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
