using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //movement fields.
    [SerializeField] private float moveSpeed = 15f;
    private Vector2 _moveInput;
    private Vector2 _delta;
    private Vector3 _newPosition;

    //bounds fields.
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private Camera _mainCamera;
    private const float SidePadding = 0.5f;
    private const float UpPadding = 0.5f;
    private const float DownPadding =2f;

    private Shooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBoundes();
    }

    // Set up the boundaries for the player's movement based on the camera's viewport.
    private void InitBoundes()
    {
        _mainCamera = Camera.main;
        if (_mainCamera != null)
        {
            _minBounds = _mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            _maxBounds = _mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        }
    }

    void Update()
    {
        Move();
    }
    
    //Get the value of the player movement that the user entered
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (_shooter != null)
        {
            _shooter.isFiring = value.isPressed;
        }
    }
    
    // Calculate the player's new position based on the movement input.
    private void Move()
    {
        _delta = (_moveInput * (moveSpeed * Time.deltaTime));
        var position = transform.position;

        // Ensure the player's new position stays within the screen boundaries.
        _newPosition.x = Mathf.Clamp(position.x + _delta.x, _minBounds.x + SidePadding, _maxBounds.x - SidePadding);
        _newPosition.y = Mathf.Clamp(position.y + _delta.y, _minBounds.y + DownPadding, _maxBounds.y - UpPadding);
        
        position = _newPosition;
        transform.position = position;
    }
}
