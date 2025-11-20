using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Bullet Settings")]
    public Transform spawnPoint;    // Point from which the bullet will be spawned
    public float fireRate = 0.5f;    // Time in seconds between shots

    [Header("Pooling Settings")]
    public ObjectPoolSpawner bulletPoolSpawner;

    public PlayerScoreUI playerScoreUI;

    private float nextFireTime = 0f;
    private bool isGameOver = false;

    private void OnEnable()
    {
        EventManager.Subscribe(EventManager.GameOverEvent, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventManager.GameOverEvent, OnGameOver);
    }

    private void OnGameOver()
    {
        isGameOver = true;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        if (Time.time >= nextFireTime)
        {
            SpawnBullet();
            nextFireTime = Time.time + fireRate; // Schedule next shot
        }
    }

    void SpawnBullet()
    {
        if (bulletPoolSpawner != null && spawnPoint != null)
        {
            GameObject bullet = bulletPoolSpawner.SpawnObject(spawnPoint.position);
            if (bullet != null)
            {
                bullet.transform.rotation = spawnPoint.rotation;
                var collision = bullet.GetComponent<BulletCollision>();
                collision.poolSpawner = bulletPoolSpawner;
                collision.playerScoreUI = playerScoreUI;
            }
        }
        else
        {
            Debug.LogWarning("PoolSpawner or SpawnPoint not assigned in BulletSpawner.");
        }
    }
}