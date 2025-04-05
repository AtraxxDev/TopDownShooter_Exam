using UnityEngine;

public class MissilePool : PoolManager
{
    [SerializeField] private float missileSpeed = 8f; 

    public GameObject FireMissile(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject missile = AskForObject(spawnPosition);
        if (missile == null) return null;

        Rigidbody rb = missile.GetComponent<Rigidbody>();
        Missile missileScript = missile.GetComponent<Missile>();

        if (missileScript != null)
        {
            missileScript.Launch(direction, this);  
        }

        rb.linearVelocity = direction * missileSpeed; 
        missile.transform.forward = direction; 

        return missile;
    }
}
