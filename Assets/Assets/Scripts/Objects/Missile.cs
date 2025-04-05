using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    private MissilePool missilePool;
    private Vector3 direction;
    private float lifetime = 5f; // Tiempo de vida del misil
    private int explosionDamage = 20;  // Da�o de la explosi�n
    private float explosionRadius = 5;  // Radio de la explosi�n

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Explode();  // Explosi�n despu�s de tiempo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Iniciar la explosi�n pero esperar 1 segundo antes de desactivar el misil
        StartCoroutine(HandleExplosion());
    }

    public void Launch(Vector3 firePointForward, MissilePool pool)
    {
        direction = firePointForward;  // Direcci�n desde el firePoint
        missilePool = pool;
        lifetime = 5f;  // Reiniciar el tiempo de vida del misil
    }

    public void SetExplosionDamage(int damage)
    {
        explosionDamage = damage;  // Asignar el da�o de la explosi�n
    }

    public void SetExplosionRadius(float radius)
    {
        explosionRadius = radius;  // Asignar el radio de la explosi�n
    }

    private void Explode()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Explosion);

        // Detectar todos los colliders en el radio de explosi�n
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Debug.Log("Explosi�n detectada: " + colliders.Length + " colisiones encontradas");

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<AIEnemyController>() != null)
            {
                Debug.Log("Enemigo con AIController encontrado");

                var damagable = collider.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    Debug.Log("Enemigo tiene el componente IDamagable, aplicando da�o");

                    damagable.TakeDamage(explosionDamage);  // Aplicar el da�o de la explosi�n

                    // Opcional: A�adir el efecto de Knockback (retroceso)
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 knockbackDirection = (collider.transform.position - transform.position).normalized;
                        rb.AddForce(knockbackDirection * 5f, ForceMode.Impulse);  // Ajusta la fuerza
                    }
                }
                else
                {
                    Debug.LogWarning("Collider encontrado pero no tiene IDamagable.");
                }
            }
        }
    }

    // Corrutina para manejar la explosi�n y la desactivaci�n despu�s de 1 segundo
    private IEnumerator HandleExplosion()
    {
        Explode();  // Ejecuta la explosi�n inmediatamente

        yield return new WaitForSeconds(0.5f);  // Espera 1 segundo

        gameObject.SetActive(false);  // Desactiva el misil despu�s de 1 segundo
    }
}
