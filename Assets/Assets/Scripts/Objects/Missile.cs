using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private MissilePool missilePool;
    private Vector3 direction;
    private float lifetime = 5f;
    private int explosionDamage = 20;
    private float explosionRadius = 5;

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(HandleExplosion());
    }

    public void Launch(Vector3 firePointForward, MissilePool pool)
    {
        direction = firePointForward;
        missilePool = pool;
        lifetime = 5f;
    }

    public void SetExplosionDamage(int damage)
    {
        explosionDamage = damage;
    }

    public void SetExplosionRadius(float radius)
    {
        explosionRadius = radius;
    }

    private void Explode()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Explosion);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<AIEnemyController>() != null)
            {
                var damagable = collider.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(explosionDamage);

                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 knockbackDirection = (collider.transform.position - transform.position).normalized;
                        rb.AddForce(knockbackDirection * 5f, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    private IEnumerator HandleExplosion()
    {
        Explode();

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
