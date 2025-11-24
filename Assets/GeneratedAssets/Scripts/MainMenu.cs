using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Gameplay";

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null && uiDocument.rootVisualElement != null)
        {
            var startButton = uiDocument.rootVisualElement.Q<Button>("startButton");
            var exitButton = uiDocument.rootVisualElement.Q<Button>("exitButton");

            if (startButton != null)
                startButton.clicked += OnStartGame;

            if (exitButton != null)
                exitButton.clicked += OnExit;
        }
    }

    private void OnStartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    private void OnExit()
    {
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.ExitPlaymode();
            return;
        }
#endif
        Application.Quit();
    }
}
