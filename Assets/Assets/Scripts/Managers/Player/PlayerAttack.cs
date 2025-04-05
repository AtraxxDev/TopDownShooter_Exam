using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private MissilePool missilePool;

    private bool canShoot = true;

    private int missileCapacity = 2; // Capacidad máxima de misiles
    private int currentMissiles = 0; // Misiles actualmente disponibles

    public void SetAttackRate(float rate)
    {
        fireRate = rate;
    }

    public void Attack()
    {
        if (!canShoot) return;

        // Disparar una bala
        GameObject bullet = bulletPool.FireBullet(firePoint.position, firePoint.forward, gameObject);
        StartCoroutine(AttackCooldown());
    }

    public void AttackMissile()
    {
        if (!canShoot || currentMissiles >= missileCapacity) return;

        // Disparar un misil
        FireMissile();
        StartCoroutine(AttackCooldown());
    }

    private void FireMissile()
    {
        // Disparar un misil desde el pool
        GameObject missile = missilePool.FireMissile(firePoint.position, firePoint.forward);

        if (missile != null)
        {
            missile.SetActive(true);
            missile.transform.position = firePoint.position;
            missile.GetComponent<Missile>().Launch(firePoint.forward, missilePool); // Lanzar misil
            currentMissiles++; // Incrementar los misiles disparados
        }
    }

    public void AddMissileCapacity(int amount)
    {
        missileCapacity += amount; // Aumentar la capacidad máxima de misiles
    }

    public void ReloadMissile(int amount)
    {
        // Recargar misiles hasta el límite de la capacidad
        currentMissiles = Mathf.Min(missileCapacity, currentMissiles + amount);
    }

    public IEnumerator AttackCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    // Método para reducir misiles cuando se recogen o explotan
    public void ReduceMissileCount()
    {
        if (currentMissiles > 0)
        {
            currentMissiles--; // Disminuir la cantidad de misiles
        }
    }
}
