using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private GameObject shooter;

    public void SetShooter(GameObject shooterObject)
    {
        shooter = shooterObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shooter) return; // Evita autodañarse

        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
        }

        gameObject.SetActive(false); 
    }


    public IEnumerator DeactivateBullet(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
       StartCoroutine(DeactivateBullet(2f)); // 2 segundos para desactivarse (ajustable)
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage; // Cambia el daño de la bala
    }
}
