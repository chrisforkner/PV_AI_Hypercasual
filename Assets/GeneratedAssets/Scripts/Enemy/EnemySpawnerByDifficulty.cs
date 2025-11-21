using UnityEngine;

public class EnemySpawnerByDifficulty : MonoBehaviour
{
    public ObjectPoolSpawner objectPoolSpawner; // Reference to the ObjectPoolSpawner component

    [Header("Spawn Rates by Difficulty")]
    public float easySpawnRate = 5f;
    public float mediumSpawnRate = 3f;
    public float hardSpawnRate = 1f;

    [Tooltip("Select difficulty: 0 = Easy, 1 = Medium, 2 = Hard")]
    public int difficultyLevel = 0;

    private float spawnTimer;
    private float currentSpawnRate;
    public float SpawnLocationZ;
    public float SpawnLocationXRange;

    private bool isGameOver = false;

    private void OnEnable()
    {
        EventManager.Subscribe(EventManager.GameOverEvent, HandleGameOver);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventManager.GameOverEvent, HandleGameOver);
    }

    private void HandleGameOver()
    {
        isGameOver = true;
    }

    private void Start()
    {
        SetSpawnRate();
        spawnTimer = currentSpawnRate;
    }

    private void Update()
    {
        if (isGameOver) return;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = currentSpawnRate;
        }
    }

    public void SetDifficulty(int difficulty)
    {
        difficultyLevel = difficulty;
        SetSpawnRate();
    }

    private void SetSpawnRate()
    {
        switch (difficultyLevel)
        {
            case 0:
                currentSpawnRate = easySpawnRate;
                break;
            case 1:
                currentSpawnRate = mediumSpawnRate;
                break;
            case 2:
                currentSpawnRate = hardSpawnRate;
                break;
            default:
                currentSpawnRate = mediumSpawnRate;
                break;
        }
    }

    private void SpawnEnemy()
    {
        if (objectPoolSpawner != null)
        {
            var spawnedEnemy = objectPoolSpawner.SpawnObject(new Vector3(Random.Range(SpawnLocationXRange * -1, SpawnLocationXRange), 0, SpawnLocationZ));
            spawnedEnemy.GetComponent<EnemyHealth>().spawner = objectPoolSpawner;
        }
        else
        {
            Debug.LogWarning("ObjectPoolSpawner reference is missing!");
        }
    }
}