using UnityEngine;

public class BulletPool : PoolManager
{
    [SerializeField] private float bulletSpeed = 10f;

    public GameObject FireBullet(Vector3 spawnPosition, Vector3 direction, GameObject shooter)
    {
        GameObject bullet = AskForObject(spawnPosition);
        if (bullet == null) return null;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetShooter(shooter);
        }

        rb.linearVelocity = direction * bulletSpeed;
        bullet.transform.forward = direction;

        return bullet;
    }
}
