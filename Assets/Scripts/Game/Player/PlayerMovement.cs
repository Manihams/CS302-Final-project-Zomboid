using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;       // Movement speed
    [SerializeField] private float _rotationSpeed = 200f; // Rotation speed for keyboard rotation

    private Vector2 _moveInput;            // Stores player movement input
    private float _rotationInput;          // Stores rotation input value
    private bool _useMouseRotation = true; // Toggle between mouse and keyboard rotation
    private Rigidbody2D _rigidbody;        // Reference to Rigidbody2D for movement
    private Animator _animator;            // Reference to Animator for animations

    // Keep player within the screen bounds
    private void ClampPositionToScreen()
    {
        Vector3 position = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

        // Clamp position to [0,1] range (screen bounds)
        viewportPos.x = Mathf.Clamp01(viewportPos.x);
        viewportPos.y = Mathf.Clamp01(viewportPos.y);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    private void Awake()
{
    // Get Rigidbody2D for movement
    _rigidbody = GetComponent<Rigidbody2D>();

    // Get Animator for controlling animations
    _animator = GetComponent<Animator>();
}

    private void Update()
    {
        // Rotate based on chosen mode
        if (_useMouseRotation)
        {
            RotateTowardMouse();
        }
        else
        {
            RotateWithKeyboard();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();              // Apply movement
        ClampPositionToScreen();   // Prevent moving off-screen
        SetAnimation();            // Update animation state
    }

    private void SetAnimation()
    {
        bool isMoving = _moveInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving); // Trigger move animation
    }

    private void MovePlayer()
    {
        // Set velocity based on input and speed
        Vector2 move = _moveInput.normalized * _moveSpeed;
        _rigidbody.linearVelocity = move;
    }

    private void RotateTowardMouse()
    {
        // Get mouse position in screen space
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convert to world position
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    
        // Get direction from player to mouse
        Vector2 direction = (worldMousePosition - transform.position).normalized;


        // Rotate to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void RotateWithKeyboard()
    {
        // Rotate using keyboard input (e.g., controller triggers)
        if (Mathf.Abs(_rotationInput) > 0.01f)
        {
            float rotationAmount = -_rotationInput * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationAmount);
        }
    }

    // Input callbacks
    private void OnMove(InputValue value)
    {
        // Movement input (WASD, joystick)
        _moveInput = value.Get<Vector2>();
    }

    private void OnRotate(InputValue value)
    {
        // Rotation input (keyboard/controller)
        _rotationInput = value.Get<float>();
    }

    private void OnToggleRotationMode()
    {
        // Toggle between mouse and keyboard rotation
        _useMouseRotation = !_useMouseRotation;
        Debug.Log("Rotation mode: " + (_useMouseRotation ? "Mouse" : "Keyboard"));
    }
}
