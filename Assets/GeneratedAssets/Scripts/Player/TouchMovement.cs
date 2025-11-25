using UnityEngine;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character movement
    private Vector2 touchPosition;
    private bool isTouching;
    private bool isGameOver;

    [Header("Animation")]
    public Animator animator; // Animator to control movement animations
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
        float directionValue = 0f;

        if (isTouching)
        {
            touchPosition = Mouse.current.position.value;
#if UNITY_ANDROID && !UNITY_EDITOR
            touchPosition = Touch.activeTouches[0].screenPosition;
#endif
            if (touchPosition.x < Screen.width / 2)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                directionValue = -1f;
            }
            else
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                directionValue = 1f;
            }
        }

        if (animator != null) animator.SetFloat("Direction", directionValue);

        if (transform.position.x < MovementRangeXMin)
        {
            transform.position = new Vector3(MovementRangeXMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x > MovementRangeXMax)
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