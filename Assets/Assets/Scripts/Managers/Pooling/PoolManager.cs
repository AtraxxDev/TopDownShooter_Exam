using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject prefabToCreate;
    public int initialPoolSize = 10;
    public List<GameObject> createdObjects = new List<GameObject>();

    public int maxPoolSize = 100; // Opcional: m�ximo de objetos que puede crear

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabToCreate);
            obj.SetActive(false);
            createdObjects.Add(obj);
        }
    }

    public GameObject AskForObject(Vector3 positionToSpawn)
    {
        if (prefabToCreate == null)
        {
            Debug.LogError("Prefab no asignado al PoolManager.");
            return null;
        }

        foreach (var obj in createdObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                obj.transform.position = positionToSpawn;
                return obj;
            }
        }

        // Si todos est�n activos y a�n no se lleg� al l�mite, crea uno nuevo
        if (createdObjects.Count < maxPoolSize)
        {
            GameObject createdObject = Instantiate(prefabToCreate, positionToSpawn, Quaternion.identity);
            createdObjects.Add(createdObject);
            return createdObject;
        }

        Debug.LogWarning("Pool alcanz� el m�ximo de objetos permitidos.");
        return null;
    }

    public virtual void StartFunction() { }
}
