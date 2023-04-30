using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaMovement : MonoBehaviour
{
    public RaycastHit2D IsGrounded { get; private set; }

    public event Action OnJump;
    public event Action OnLand;
    public event Action OnOverChimneyEnter;
    public event Action OnChimneyJump;

    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 120f;
    [SerializeField] private float chimneyJumpForce = 60f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float lowJumpMultiplier = 4f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float coyoteTime = 0.25f;
    [SerializeField] private float jumpBuffer = 0.25f;
    [SerializeField] private float chimneyJumpWindow = 0.25f;

    [Header("References")]
    [SerializeField] private BoxCollider2D touchCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask chimneyLayer;

    private Rigidbody2D _rigidbody;

    private float _coyoteTimer;
    private float _jumpBufferTimer;
    private float _chimneyTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);

        var newIsGrounded = IsTouching(groundLayer);
        if (!IsGrounded && newIsGrounded)
        {
            OnLand?.Invoke();
        }
        IsGrounded = newIsGrounded;
        
        if (IsGrounded) { _coyoteTimer = coyoteTime; }
        else            { _coyoteTimer -= Time.deltaTime; }

        if (Input.GetButtonDown("Jump")) { _jumpBufferTimer = jumpBuffer; }
        else                             { _jumpBufferTimer -= Time.deltaTime; }
        
        if (_chimneyTimer > 0)
        {
            _chimneyTimer -= Time.deltaTime;
        }
        
        if (_jumpBufferTimer > 0)
        {
            if (_coyoteTimer > 0)
            {
                Jump(jumpForce);
            }
            else if (_chimneyTimer > 0)
            {
                Jump(chimneyJumpForce);
                OnChimneyJump?.Invoke();
            }
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

    private void Jump(float force)
    {
        // Note, the sack and arms will still have downward velocity
        // For consistency we might want to consider also setting those to 0
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        OnJump?.Invoke();
        _jumpBufferTimer = 0;
        _coyoteTimer = 0;
        _chimneyTimer = 0;
    }
    
    private RaycastHit2D IsTouching(LayerMask mask)
    {
        var bounds = touchCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0f, mask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!TryGetComponent<Collider2D>(out var coll)) return;
        if (!coll.IsTouchingLayers(chimneyLayer)) return;
        
        OnOverChimneyEnter?.Invoke();
        _chimneyTimer = chimneyJumpWindow;
    }
}
