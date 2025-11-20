using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    private int initialHealth;
    public ObjectPoolSpawner spawner;

    private void Awake()
    {
        initialHealth = health;
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        health = initialHealth;
        Debug.Log($"{gameObject.name} health reset to {health}.");
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        spawner.ReturnObjectToPool(gameObject);
    }
}