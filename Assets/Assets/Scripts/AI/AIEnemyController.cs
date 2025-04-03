using UnityEngine;

public class AIEnemyController : MonoBehaviour
{
    [SerializeField] private StatsDataSO stats;
    [SerializeField] private float attackRange;
    [SerializeField] private BulletPool bulletPool;

    private Transform playerTarget;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyHealth = GetComponent<EnemyHealth>();

        enemyMovement.Initialize(stats.moveSpeed);
        enemyAttack.Initialize(stats.attackRate, bulletPool, gameObject); // Se pasa BulletPool
        enemyHealth.Initialize(stats.maxHealth);

        playerTarget = FindFirstObjectByType<PlayerController>().transform;
    }



    private void Update()
    {
        if (playerTarget == null) return;

        Vector3 direction = (playerTarget.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, playerTarget.position);

        // El enemigo siempre se mueve hacia el jugador
        enemyMovement.Move(direction);

        if (distance <= attackRange)
        {
            enemyAttack.Shoot(playerTarget.position);  // Dispara cuando está dentro del rango
        }
    }

    public void SetDifficulty(float speed, float damage, float health)
    {
        // Actualiza las estadísticas del enemigo basadas en la dificultad de la oleada
        enemyMovement.Initialize(speed); // Ajusta la velocidad de movimiento
        enemyAttack.SetBulletDamage((int)damage);   // Ajusta el daño de las balas
        enemyHealth.Initialize(health); // Ajusta la salud de los enemigos por oleada
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
