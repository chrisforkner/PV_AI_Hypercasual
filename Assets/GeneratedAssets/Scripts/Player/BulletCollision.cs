using UnityEngine;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    public string enemyTag = "Enemy"; // Tag assigned to enemy GameObjects
    public ObjectPoolSpawner poolSpawner; // Reference to the ObjectPoolSpawner

    [Header("Damage Settings")]
    public int bulletDamage = 25; // Damage dealt to enemy

    public ObjectPoolSpawner explosionSpawner;
    public PlayerScoreUI playerScoreUI; // Reference to PlayerScoreUI script to update score

    private Coroutine despawnCoroutine;

    private void OnEnable()
    {
        // Start coroutine to despawn bullet after 3 seconds
        despawnCoroutine = StartCoroutine(DespawnAfterDelay(3f));
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        despawnCoroutine = null;
        DespawnBullet();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            Debug.Log("Bullet hit an enemy (trigger)!");

            EventManager.TriggerEvent(EventManager.EnemyDefeatedEvent, other.transform.position);

            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
                // Trigger score update event instead of directly updating PlayerScoreUI
                EventManager.TriggerEvent<int>(EventManager.EnemyScoreEvent, 100);
            }
            DespawnBullet();
        }
    }

    private void DespawnBullet()
    {
        if (poolSpawner != null)
        {
            poolSpawner.ReturnObjectToPool(gameObject);
        }
        else
        {
            Debug.LogWarning("No ObjectPoolSpawner assigned to BulletCollision. Destroying instead.");
            Destroy(gameObject);
        }

        if(despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }
    }
}