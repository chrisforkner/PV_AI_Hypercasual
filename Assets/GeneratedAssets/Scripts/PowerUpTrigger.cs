using UnityEngine;

public class PowerUpTrigger : MonoBehaviour
{
    public ObjectPoolSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("PowerUp collected: " + other.name);
            EventManager.TriggerEvent(EventManager.PowerUpHit);
            // Add your power-up effect logic here
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("Wall triggered by PowerUpTrigger: " + other.name);
            spawner.ReturnObjectToPool(gameObject);
        }
    }
}