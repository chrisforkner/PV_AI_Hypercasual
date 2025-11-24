using UnityEngine;

public class EventAudioPlayer : MonoBehaviour
{
    [Tooltip("Assign an AudioSource that will play the bullet shot sound")]
    public AudioSource audioSource;
    [Tooltip("Assign the AudioClip to play when a bullet is shot")]
    public AudioClip bulletShotClip;

    [Tooltip("The EventManager event name to subscribe to")]
    public string eventName = EventManager.BulletShot;

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(eventName))
        {
            EventManager.Subscribe(eventName, OnEventTriggered);
        }
    }

    private void OnDisable()
    {
        if (!string.IsNullOrEmpty(eventName))
        {
            EventManager.Unsubscribe(eventName, OnEventTriggered);
        }
    }

    private void OnEventTriggered()
    {
        if (audioSource != null && bulletShotClip != null)
        {
            audioSource.PlayOneShot(bulletShotClip);
        }
        else
        {
            Debug.LogWarning("BulletShotAudio missing AudioSource or AudioClip assignment.");
        }
    }
}