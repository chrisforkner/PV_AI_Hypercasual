// 11/18/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ObjectPoolSpawner : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public GameObject prefabToPool; // The prefab to pool
    public int poolSize = 10; // Number of objects in the pool
    public float SpawnLocationZ;
    public float SpawnLocationXRange;
    private Queue<GameObject> objectPool;

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabToPool);
            obj.SetActive(false); // Deactivate the object initially
            objectPool.Enqueue(obj);
        }
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.transform.position = new Vector3(Random.Range(SpawnLocationXRange * -1, SpawnLocationXRange), 0, SpawnLocationZ);
            //obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Object pool is empty!");
            return null;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
