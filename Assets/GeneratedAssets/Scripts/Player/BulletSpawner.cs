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
        EventManager.Subscribe(EventManager.PowerUpHit, OnPowerUpHit);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventManager.GameOverEvent, OnGameOver);
        EventManager.Unsubscribe(EventManager.PowerUpHit, OnPowerUpHit);
    }

    [Header("PowerUp Settings")]
    public float PowerUpIncrease = 0.1f; // Amount to decrease fire rate when a power-up is hit (lower fireRate means faster shooting)

    private void OnPowerUpHit()
    {
        Debug.Log("PowerUpHit event received in BulletSpawner.");
        fireRate = Mathf.Max(0.01f, fireRate - PowerUpIncrease); // Reduce fireRate to shoot faster, with a lower limit
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
                EventManager.TriggerEvent(EventManager.BulletShot);
            }
        }
        else
        {
            Debug.LogWarning("PoolSpawner or SpawnPoint not assigned in BulletSpawner.");
        }
    }
}