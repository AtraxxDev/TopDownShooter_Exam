using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject prefabToCreate;
    public int initialPoolSize = 10; 
    public List<GameObject> createdObjects;


    private void Start()
    {
        InitializePool(); // Inicializa el pool al iniciar
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabToCreate);
            obj.SetActive(false); // Lo desactivamos para que esté disponible en el pool
            createdObjects.Add(obj);
        }
    }

    // Método que pedirá un objeto del pool, si no hay disponibles, crea uno nuevo
    public GameObject AskForObject(Vector3 positionToSpawn)
    {
        foreach (var obj in createdObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = positionToSpawn;
                return obj;
            }
        }

        // Si no se encuentra ninguno, crear un nuevo objeto
        GameObject createdObject = Instantiate(prefabToCreate, positionToSpawn, Quaternion.identity);
        createdObjects.Add(createdObject);
        return createdObject;
    }

    public virtual void StartFunction() { }
}
