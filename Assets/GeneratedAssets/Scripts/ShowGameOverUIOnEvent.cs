using UnityEngine;
using UnityEngine.UIElements;

public class ShowGameOverUIOnEvent : MonoBehaviour
{
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogWarning("UIDocument component not found on ShowGameOverUIOnEvent GameObject.");
        }
    }

    private void OnEnable()
    {
        EventManager.Subscribe(EventManager.GameOverEvent, OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventManager.GameOverEvent, OnGameOver);
    }

    private void OnGameOver()
    {
        if (uiDocument != null)
        {
            uiDocument.enabled = true;
        }
    }
}