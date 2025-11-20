using UnityEngine;

public class BackWallTrigger : MonoBehaviour
{
    public ObjectPoolSpawner EnemySpawner;
    public PlayerHealthUI PlayerHealthUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"BackWall detected collision with enemy: {other.name}");
            // Add logic here for what should happen when an enemy triggers the collision.

            EnemySpawner.ReturnObjectToPool(other.gameObject);

            EventManager.TriggerEvent<float>(EventManager.BackWallTriggeredEvent, 1.0f);
        }
    }
}