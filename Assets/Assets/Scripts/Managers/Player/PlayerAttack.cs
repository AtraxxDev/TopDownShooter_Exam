using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private BulletPool bulletPool;

    private bool canShoot = true;

    public void SetAttackRate(float rate)
    {
        fireRate = rate;
    }
    public void Attack()
    {
        if (!canShoot) return;

        GameObject bullet = bulletPool.FireBullet(firePoint.position, firePoint.forward, gameObject);
        StartCoroutine(AttackCooldown());
    }

    public IEnumerator AttackCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
