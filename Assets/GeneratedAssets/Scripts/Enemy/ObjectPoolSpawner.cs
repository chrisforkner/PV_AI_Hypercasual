// 11/18/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolSpawner : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public GameObject prefabToPool; // The prefab to pool
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    // Lazy-load: only instantiate when needed
    public GameObject SpawnObject(Vector3 position)
    {
        GameObject obj;

        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
        }
        else
        {
            obj = Instantiate(prefabToPool);
        }

        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}