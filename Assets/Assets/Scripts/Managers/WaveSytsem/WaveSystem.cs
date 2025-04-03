using System;
using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public event Action<int> OnWaveStarted;
    public event Action<int> OnWaveCompleted;

    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float delayBeforeSpawn = 2f; // Tiempo de espera antes de empezar a spawnnear
    [SerializeField] private Transform[] spawnPoints; // Puntos de spawn predefinidos

    private int waveId = 0;
    private int remainingEnemies = 0;
    private bool waveInProgress = false;
    private bool waitingForShop = false;

    private void Start()
    {
        // StartCoroutine(StartWave()); No es necesario iniciar la oleada al principio
        StartNewWave();
    }

    // Método para iniciar una nueva oleada
    [ContextMenu("StartNewWave")]
    public void StartNewWave()
    {
        if (waveInProgress)
        {
            Debug.Log("Ya hay una oleada en progreso.");
            return;
        }

        waveId++;
        waveInProgress = true;
        waitingForShop = false;
        WaveData waveData = GenerateWaveData(waveId);
        remainingEnemies = waveData.enemyCount;

        OnWaveStarted?.Invoke(waveId);
        StartCoroutine(DelayBeforeSpawnAndStartWave(waveData));
    }

    // Método para esperar un tiempo antes de empezar a spawnear enemigos
    private IEnumerator DelayBeforeSpawnAndStartWave(WaveData waveData)
    {
        Debug.Log("Esperando antes de comenzar la oleada...");
        yield return new WaitForSeconds(delayBeforeSpawn); // Espera antes de empezar a spawnear enemigos

        // Luego de la espera, comenzamos a spawnear los enemigos
        yield return StartCoroutine(SpawnEnemies(waveData));
    }

    private IEnumerator SpawnEnemies(WaveData waveData)
    {
        for (int i = 0; i < waveData.enemyCount; i++)
        {
            SpawnEnemy(waveData);
            yield return new WaitForSeconds(spawnInterval); // Intervalo entre enemigos
        }
    }

    private void SpawnEnemy(WaveData waveData)
    {
        Vector3 spawnPosition = GetSpawnPoint();
        GameObject enemy = enemyPool.GetEnemy(spawnPosition);

        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        var enemyController = enemy.GetComponent<AIEnemyController>();

        enemyHealth.Initialize(waveData.enemyHealth);
        enemyController.SetDifficulty(waveData.enemySpeed, waveData.enemyDamage, waveData.enemyHealth);

        // Desuscribirse antes de suscribirse para evitar múltiples suscripciones.
        enemyHealth.OnEnemyDeath -= HandleEnemyDeath;
        enemyHealth.OnEnemyDeath += HandleEnemyDeath;
    }


    private void HandleEnemyDeath()
    {
        remainingEnemies--;
        if (remainingEnemies <= 0)
        {
            remainingEnemies = 0;
            waveInProgress = false;
            OnWaveCompleted?.Invoke(waveId);

            Debug.Log($"Wave {waveId} completada. Abrir tienda...");

            waitingForShop = true; // Se detiene el inicio de la siguiente oleada hasta que el jugador continúe
        }
    }

    private WaveData GenerateWaveData(int waveId)
    {
        return new WaveData
        {
            enemyCount = 5 + waveId, // Aumenta el número de enemigos con cada oleada
            enemyHealth = 1 + (waveId * 2), // Aumenta la vida de los enemigos
            enemySpeed = 3f + (waveId * 0.1f), // Aumenta la velocidad de los enemigos
            enemyDamage = 10 + (waveId * 2) // Aumenta el daño de los enemigos
        };
    }

    private Vector3 GetSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No hay puntos de spawn asignados en WaveSystem.");
            return Vector3.zero;
        }

        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
}

public struct WaveData
{
    public int enemyCount;
    public float enemyHealth;
    public float enemySpeed;
    public float enemyDamage;

    public WaveData(int enemyCount, float enemyHealth, float enemySpeed, float enemyDamage)
    {
        this.enemyCount = enemyCount;
        this.enemyHealth = enemyHealth;
        this.enemySpeed = enemySpeed;
        this.enemyDamage = enemyDamage;
    }
}
