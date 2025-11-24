using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScoreUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label scoreLabel;
    private int score;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component is missing on GameObject.");
            return;
        }

        // Get the rootVisualElement directly from the UI Document
        var root = uiDocument.rootVisualElement;

        // Find score label by name (must match the name in UXML)
        scoreLabel = root.Q<Label>("score-label");
        if (scoreLabel == null)
        {
            Debug.LogError("ScoreLabel not found in PlayerStatusHUD.");
        }

        // Subscribe to event when enabled
    }

    private void OnEnable()
    {
        EventManager.Subscribe<int>(EventManager.EnemyScoreEvent, OnEnemyScore);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe<int>(EventManager.EnemyScoreEvent, OnEnemyScore);
    }

    private void OnEnemyScore(int amount)
    {
        AddScore(amount);

        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = $"Score: {score}";
        }
    }
}