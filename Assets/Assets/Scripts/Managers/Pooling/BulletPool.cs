using UnityEngine;

public class BulletPool : PoolManager
{
    [SerializeField] private float bulletSpeed = 10f;

    public GameObject FireBullet(Vector3 spawnPosition, Vector3 direction, GameObject shooter)
    {
        GameObject bullet = AskForObject(spawnPosition); // Obtiene una bala del pool
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetShooter(shooter); // Evita que el disparador se haga daño
        }

        rb.linearVelocity = direction * bulletSpeed;
        bullet.transform.forward = direction; // Orienta la bala

        return bullet;
    }
}
