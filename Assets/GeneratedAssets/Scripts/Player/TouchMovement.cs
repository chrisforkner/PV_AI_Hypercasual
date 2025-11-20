// 11/18/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character movement
    private Vector2 touchPosition;
    private bool isTouching;
    private bool isGameOver;
    public float MovementRangeXMin;
    public float MovementRangeXMax;

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
        isGameOver = true;
    }

    private void Update()
    {
        if (isGameOver) return;

        isTouching = Mouse.current.press.isPressed || Touch.activeTouches.Count > 0;

        if (isTouching)
        {
            touchPosition = Mouse.current.position.value;
            
#if UNITY_ANDROID && !UNITY_EDITOR
            touchPosition = Touch.activeTouches[0].screenPosition;
#endif

            // Check if the touch is on the left or right side of the screen
            if (touchPosition.x > Screen.width / 2)
            {
                // Move left
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Move right
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }

            if(transform.position.x < MovementRangeXMin)
            {
                transform.position = new Vector3(MovementRangeXMin, transform.position.y, transform.position.z);
            }

            if(transform.position.x > MovementRangeXMax)
            {
                transform.position = new Vector3(MovementRangeXMax, transform.position.y, transform.position.z);
            }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isTouching = true;
            touchPosition = context.ReadValue<Vector2>();
            Debug.Log("Started Touching");
        }
        else if (context.canceled)
        {
            isTouching = false;
            Debug.Log("Ended Touching");
        }
    }
}
