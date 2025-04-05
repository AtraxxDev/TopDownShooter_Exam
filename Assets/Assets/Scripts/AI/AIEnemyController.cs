using UnityEngine;

public class AIEnemyController : MonoBehaviour
{
    [SerializeField] private StatsDataSO stats;
    [SerializeField] private float attackRange;
    private BulletPool bulletPool;

    private Transform playerTarget;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;
    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerAnimations = GetComponent<PlayerAnimations>();
        bulletPool = FindFirstObjectByType<BulletPool>();

        enemyMovement.Initialize(stats.moveSpeed);
        enemyAttack.Initialize(stats.attackRate, bulletPool, gameObject); 
        enemyHealth.Initialize(stats.maxHealth);

        playerTarget = FindFirstObjectByType<PlayerController>().transform;
    }



    private void Update()
    {
        if (playerTarget == null) return;

        Vector3 direction = (playerTarget.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        enemyMovement.Move(direction);
        playerAnimations.PlayAnimation(PlayerAnimationState.Walk);

        if (distance <= attackRange)
        {
            enemyAttack.Shoot(playerTarget.position);  
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot);
        }
        else
        {
            playerAnimations.PlayAnimation(PlayerAnimationState.Shoot,false);
        }
    }

    public void SetDifficulty(float speed, float damage, float health)
    {
        enemyMovement.Initialize(speed);
        enemyAttack.SetBulletDamage((int)damage);   // Ajusta el daño de las balas
        enemyHealth.Initialize(health); // Ajusta la salud de los enemigos por oleada
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
