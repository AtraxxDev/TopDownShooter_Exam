using UnityEngine;

public class EnemyPool : PoolManager
{
    public GameObject GetEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = AskForObject(spawnPosition);
        return enemy;
    }
}
