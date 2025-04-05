using UnityEngine;

public class MissilePool : PoolManager
{
    [SerializeField] private float missileSpeed = 8f;  // Velocidad del misil

    public GameObject FireMissile(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject missile = AskForObject(spawnPosition);
        if (missile == null) return null;

        Rigidbody rb = missile.GetComponent<Rigidbody>();
        Missile missileScript = missile.GetComponent<Missile>();

        if (missileScript != null)
        {
            missileScript.Launch(direction, this);  // Lanzar el misil
        }

        rb.linearVelocity = direction * missileSpeed;  // Asignar la velocidad al misil
        missile.transform.forward = direction;  // Asegurar que el misil mire en la direcci�n correcta

        return missile;
    }
}
