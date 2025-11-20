using UnityEngine;
using System.Collections;

public class AutoReturnToPool : MonoBehaviour
{
    private ObjectPoolSpawner poolSpawner;

    public void Initialize(ObjectPoolSpawner spawner)
    {
        poolSpawner = spawner;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnAfterDelay(2f));
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (poolSpawner != null)
        {
            poolSpawner.ReturnObjectToPool(gameObject);
        }
        else
        {
            Debug.LogWarning($"AutoReturnToPool: No ObjectPoolSpawner reference set for {gameObject.name}.");
        }
    }
}