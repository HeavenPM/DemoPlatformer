using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerJump : MonoBehaviour
{
    public bool IsGrounded => _isGrounded;

    [SerializeField] private float _jumpForce;

    private bool _isGrounded;
    private bool _isDoubleJumpReady = true;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded )
        {
            Jump();
            _isGrounded = false;
        }
        else if (Input.GetButtonDown("Jump") && _isDoubleJumpReady )
        {
            Jump();
            _isDoubleJumpReady = false;
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _isGrounded = true;
            _isDoubleJumpReady = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _isGrounded = false;
        }
    }
}
