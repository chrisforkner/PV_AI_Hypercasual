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

    // Optional: Method to set the speed dynamically
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
