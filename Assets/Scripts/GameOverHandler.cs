using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isGameOver = false;

    private void OnEnable()
    {
        GameEvents.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= HandleGameOver;
    }

    public void Update()
    {
        if(isGameOver && Mouse.current.press.isPressed || Touch.activeTouches.Count > 0)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private void HandleGameOver()
    {
        isGameOver = true;
    }
}