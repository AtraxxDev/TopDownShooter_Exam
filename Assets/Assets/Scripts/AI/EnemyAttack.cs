using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private BulletPool bulletPool;

    private float attackRate;
    private float lastAttackTime;
    private GameObject shooter;

    public void Initialize(float rate, BulletPool pool, GameObject shooterReference)
    {
        attackRate = rate;
        bulletPool = pool;
        shooter = shooterReference;
    }

    public void Shoot(Vector3 targetPosition)
    {
        if (Time.time - lastAttackTime >= attackRate)
        {
            lastAttackTime = Time.time;

            // Dirección del disparo
            Vector3 direction = (targetPosition - firePoint.position).normalized;

            // Disparar usando el BulletPool
            bulletPool.FireBullet(firePoint.position, direction, shooter);
        }
    }
}
