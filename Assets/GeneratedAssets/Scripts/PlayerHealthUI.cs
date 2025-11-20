using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private VisualElement healthFill;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }
    }

    private void Start()
    {
        var root = uiDocument.rootVisualElement;
        healthFill = root.Q<VisualElement>("health-fill");
        UpdateHealthUI();
    }

    public void ReduceHealth(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            EventManager.TriggerEvent(EventManager.GameOverEvent);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthFill != null)
        {
            float healthPercent = (currentHealth / maxHealth) * 100f;
            healthFill.style.width = new Length(healthPercent, LengthUnit.Percent);
        }
    }

    private void OnEnable()
    {
        EventManager.Subscribe<float>(EventManager.BackWallTriggeredEvent, OnBackWallTriggered);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe<float>(EventManager.BackWallTriggeredEvent, OnBackWallTriggered);
    }

    private void OnBackWallTriggered(float amount)
    {
        ReduceHealth(amount);
    }
}