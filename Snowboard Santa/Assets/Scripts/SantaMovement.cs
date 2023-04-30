using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaMovement : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public event Action OnJump; 

    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 80f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float lowJumpMultiplier = 4f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float coyoteTime = 0.25f;
    [SerializeField] private float jumpBuffer = 0.25f;

    [Header("References")]
    [SerializeField] private BoxCollider2D groundedCollider;
    [SerializeField] private LayerMask jumpableLayers;

    private Rigidbody2D _rigidbody;

    private float _coyoteTimer;
    private float _jumpBufferTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        IsGrounded = CheckGrounded();
        
        if (IsGrounded)
        {
            _coyoteTimer = coyoteTime;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferTimer = jumpBuffer;
        }
        else
        {
            _jumpBufferTimer -= Time.deltaTime;
        }
        
        if (_jumpBufferTimer > 0 && _coyoteTimer > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            OnJump?.Invoke();
            _jumpBufferTimer = 0;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _coyoteTimer = 0;
        }

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }
        else if (!Input.GetButton("Jump"))
        {
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
        
        var horizontal = Input.GetAxisRaw("Horizontal");
        _rigidbody.angularVelocity = -horizontal * rotationSpeed;
    }

    private bool CheckGrounded()
    {
        var bounds = groundedCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0f, jumpableLayers);
    }
}
