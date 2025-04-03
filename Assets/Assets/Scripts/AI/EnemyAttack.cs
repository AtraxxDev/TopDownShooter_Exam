using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    private BulletPool bulletPool;

    private float attackRate;
    private float lastAttackTime;
    private GameObject shooter;
    private int attackDamage;

    public void Initialize(float rate, BulletPool pool, GameObject shooterReference)
    {
        attackRate = rate;
        bulletPool = pool;
        shooter = shooterReference;
    }

    public void SetBulletDamage(int damage)
    {
        attackDamage = damage; // Cambia el daño de las balas cuando el enemigo sube de nivel
    }

    public void Shoot(Vector3 targetPosition)
    {
        if (Time.time - lastAttackTime >= attackRate)
        {
            lastAttackTime = Time.time;

            // Dirección del disparo
            Vector3 direction = (targetPosition - firePoint.position).normalized;

            // Crear la bala desde el BulletPool y asignar el daño al Bullet
            GameObject bullet = bulletPool.FireBullet(firePoint.position, direction, shooter);
            bullet.GetComponent<Bullet>().SetDamage(attackDamage); // Se asigna el daño de la bala
        }
    }
}
