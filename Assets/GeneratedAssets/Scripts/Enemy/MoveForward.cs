// 11/18/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Configurable speed value
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        // Move the transform forward in the positive Z direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        speed = 0f;
    }

    // Optional: Method to set the speed dynamically
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
