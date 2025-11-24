using UnityEngine;

public class SpawnerByDifficulty : MonoBehaviour
{
    public ObjectPoolSpawner objectPoolSpawner; // Reference to the ObjectPoolSpawner component

    [Header("Spawn Rates by Difficulty")]
    public float easySpawnRate = 5f;
    public float mediumSpawnRate = 3f;
    public float hardSpawnRate = 1f;
    public float veryHardSpawnRate = 0.8f;
    public float extremeSpawnRate = 0.5f;
    public float impossibleSpawnRate = 0.2f;

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard,
        VeryHard,
        Extreme,
        Impossible
    }

    public DifficultyLevel difficultyLevel = DifficultyLevel.Easy;

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

    [Header("Auto Difficulty Settings")]
    public bool autoIncreaseDifficulty = false;
    public float difficultyIncreaseInterval = 30f; // seconds
    private float difficultyTimer = 0f;

    private void Update()
    {
        if (isGameOver) return;

        if (autoIncreaseDifficulty)
        {
            difficultyTimer += Time.deltaTime;
            if (difficultyTimer >= difficultyIncreaseInterval)
            {
                difficultyTimer = 0f;
                // Increase difficulty up to Hard
                var maxDifficultyIndex = System.Enum.GetValues(typeof(DifficultyLevel)).Length - 1;
                if ((int)difficultyLevel < maxDifficultyIndex)
                {
                    difficultyLevel++;
                    SetSpawnRate();
                    Debug.Log("Auto difficulty increased to: " + difficultyLevel);
                }
            }
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnObject();
            spawnTimer = currentSpawnRate;
        }
    }

    public void SetDifficulty(int difficulty)
    {
        difficultyLevel = (DifficultyLevel)difficulty;
        SetSpawnRate();
    }

    private void SetSpawnRate()
    {
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                currentSpawnRate = easySpawnRate;
                break;
            case DifficultyLevel.Medium:
                currentSpawnRate = mediumSpawnRate;
                break;
            case DifficultyLevel.Hard:
                currentSpawnRate = hardSpawnRate;
                break;
            case DifficultyLevel.VeryHard:
                currentSpawnRate = veryHardSpawnRate;
                break;
            case DifficultyLevel.Extreme:
                currentSpawnRate = extremeSpawnRate;
                break;
            case DifficultyLevel.Impossible:
                currentSpawnRate = impossibleSpawnRate;
                break;
            default:
                currentSpawnRate = mediumSpawnRate;
                break;
        }
    }

    private void SpawnObject()
    {
        if (objectPoolSpawner != null)
        {
            var spawnedEnemy = objectPoolSpawner.SpawnObject(new Vector3(Random.Range(SpawnLocationXRange * -1, SpawnLocationXRange), 0, SpawnLocationZ));
            var enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.spawner = objectPoolSpawner;
            }
            var powerUpTrigger = spawnedEnemy.GetComponent<PowerUpTrigger>();
            if(powerUpTrigger != null)
            {
                powerUpTrigger.spawner = objectPoolSpawner;
            }
        }
        else
        {
            Debug.LogWarning("ObjectPoolSpawner reference is missing!");
        }
    }
}