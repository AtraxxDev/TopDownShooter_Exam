using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private MissilePool missilePool;

    private bool canShoot = true;

    public int missileCapacity = 2; // Capacidad máxima de misiles
    public int currentMissiles = 2; // Misiles actualmente disponibles (inicia con 2)

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
        if (!canShoot || currentMissiles <= 0) return; 

        // Disparar un misil
        FireMissile();
        StartCoroutine(AttackCooldown());
    }

    private void FireMissile()
    {
        GameObject missile = missilePool.FireMissile(firePoint.position, firePoint.forward);

        if (missile != null)
        {
            missile.SetActive(true);
            missile.transform.position = firePoint.position;
            missile.GetComponent<Missile>().Launch(firePoint.forward, missilePool);
            ReduceMissileCount();
        }
    }

    public void AddMissileCapacity(int amount)
    {
        missileCapacity += amount;
    }

    public void ReloadMissiles()
    {
        currentMissiles = missileCapacity; 
    }

    public IEnumerator AttackCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void ReduceMissileCount()
    {
        if (currentMissiles > 0)
        {
            currentMissiles--; 
        }
    }
}
