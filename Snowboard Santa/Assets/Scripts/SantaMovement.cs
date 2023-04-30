using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SantaMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 80f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float lowJumpMultiplier = 4f;
    [SerializeField] private float rotationSpeed = 500f;

    [Header("References")]
    [SerializeField] private BoxCollider2D groundedCollider;
    [SerializeField] private LayerMask jumpableLayers; 
    
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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

    private bool IsGrounded()
    {
        var bounds = groundedCollider.bounds;
        return Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down, 0f, jumpableLayers);
    }
}
