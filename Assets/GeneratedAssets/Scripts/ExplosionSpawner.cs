using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolSpawner objectPoolSpawner;
    [SerializeField] private Vector3 spawnOffset;

    private void OnEnable()
    {
        EventManager.Subscribe<Vector3>(EventManager.EnemyDefeatedEvent, OnEnemyDefeated);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe<Vector3>(EventManager.EnemyDefeatedEvent, OnEnemyDefeated);
    }

    private void OnEnemyDefeated(Vector3 enemyPosition)
    {
        Debug.Log("ExplosionSpawner.OnEnemyDefeated " + enemyPosition);
        if (objectPoolSpawner != null)
        {
            Vector3 spawnPosition = enemyPosition + spawnOffset;
            GameObject explosion = objectPoolSpawner.SpawnObject(spawnPosition);
            if (explosion != null)
            {
                explosion.GetComponent<AutoReturnToPool>().Initialize(objectPoolSpawner);
            }
        }
        else
        {
            Debug.LogWarning("ObjectPoolSpawner is not assigned in ExplosionSpawner.");
        }
    }
}