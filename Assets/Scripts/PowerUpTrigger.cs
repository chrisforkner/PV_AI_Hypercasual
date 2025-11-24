using UnityEngine;

public class PowerUpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log("PowerUp collected: " + other.name);
            EventManager.TriggerEvent(EventManager.PowerUpHit);
            // Add your power-up effect logic here
        }
    }
}