using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 200f;

    private Vector2 _moveInput;
    private float _rotationInput;
    private bool _useMouseRotation = true;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    

    private void ClampPositionToScreen()
    {
        Vector3 position = transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

        viewportPos.x = Mathf.Clamp01(viewportPos.x);
        viewportPos.y = Mathf.Clamp01(viewportPos.y);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        MovePlayer();
        ClampPositionToScreen();
        SetAnimation();
    }

    private void SetAnimation()
    {
        bool isMoving = _moveInput != Vector2.zero;

        _animator.SetBool("IsMoving", isMoving);
    }

    private void MovePlayer()
    {
        Vector2 move = _moveInput.normalized * _moveSpeed;
        _rigidbody.linearVelocity = move;
    }

    private void RotateTowardMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldMousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void RotateWithKeyboard()
    {
        if (Mathf.Abs(_rotationInput) > 0.01f)
        {
            float rotationAmount = -_rotationInput * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationAmount);
        }
    }

    // Input Action Callbacks
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    private void OnRotate(InputValue value)
    {
        _rotationInput = value.Get<float>();
    }

    private void OnToggleRotationMode()
    {
        _useMouseRotation = !_useMouseRotation;
        Debug.Log("Rotation mode: " + (_useMouseRotation ? "Mouse" : "Keyboard"));
    }
}




