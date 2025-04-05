using UnityEngine;

public class EnemyPool : PoolManager
{
    public GameObject GetEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = AskForObject(spawnPosition);
        if (enemy == null) return null;
        return enemy;
    }
}
