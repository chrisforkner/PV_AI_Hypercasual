using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class PlayVFXOnEnable : MonoBehaviour
{
    [Tooltip("Visual Effect to play on enable. Will auto-assign if left empty.")]
    [SerializeField] private VisualEffect visualEffect;

    private void OnEnable()
    {
        if (visualEffect != null)
        {
            visualEffect.Play();
        }
        else
        {
            Debug.LogWarning($"{nameof(PlayVFXOnEnable)} on {gameObject.name} has no VisualEffect assigned.");
        }
    }
}