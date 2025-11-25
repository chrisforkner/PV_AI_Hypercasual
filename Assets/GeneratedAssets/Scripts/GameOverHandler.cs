using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isGameOver = false;
    private float gameOverTime;

    private void OnEnable()
    {
        EventManager.Subscribe(EventManager.GameOverEvent, HandleGameOver);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventManager.GameOverEvent, HandleGameOver);
    }

    public void Update()
    {
        if ((Mouse.current.press.isPressed || Touch.activeTouches.Count > 0) && isGameOver && Time.time - gameOverTime >= 5f)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private void HandleGameOver()
    {
        isGameOver = true;
        gameOverTime = Time.time;
    }
}