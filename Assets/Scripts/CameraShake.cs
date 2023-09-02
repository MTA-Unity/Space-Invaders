using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _originalPosition;
    private float _shakeDuration;
    private float _shakeMagnitude;
    private const float DampingSpeed = 1.0f;
    private void Awake()
    {
        _cameraTransform = GetComponent<Transform>(); 
    }

    private void OnEnable()
    {// Store the original camera position when script is enabled
        _originalPosition = _cameraTransform.localPosition; 
    }

    private void Update()
    {
        if (_shakeDuration > 0)
        {// Apply a random displacement within a sphere to simulate camera shake
            _cameraTransform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeMagnitude;
            _shakeDuration -= Time.deltaTime * DampingSpeed;
        }
        else
        {// Reset camera position to original after shake duration expires
            _shakeDuration = 0f;
            _cameraTransform.localPosition = _originalPosition;
        }
    }

    // Start a camera shake with given duration and magnitude
    public void ShakeCamera(float duration, float magnitude)
    {
        _shakeDuration = duration;
        _shakeMagnitude = magnitude;
    }
}