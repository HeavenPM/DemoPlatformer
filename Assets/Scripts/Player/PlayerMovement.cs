using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    public bool IsLookingRight => _isLookingRight;
    public bool IsMoving => _isMoving;

    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody2D;
    private Transform _characterTransform;
    private float _horizontalDirection;
    private bool _isLookingRight = true;
    private bool _isSliding = false;
    private bool _isMoving = false;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterTransform = transform;
    }

    private void Update()
    {
        Move();
        findOutDirection();
    }

    private void Move()
    {
        _isMoving = Input.GetAxis("Horizontal") != 0 ? true : false;

        if (!GetComponent<PlayerAttack>().IsAttacking && !_isSliding)
        {
            _horizontalDirection = Input.GetAxis("Horizontal");
            _rigidbody2D.velocity = new Vector2(_horizontalDirection * _moveSpeed, _rigidbody2D.velocity.y);
        }
        else if (!GetComponent<PlayerAttack>().IsAttacking && _isSliding)
        {
            if (_isLookingRight)
            {
                _horizontalDirection = Input.GetAxis("Horizontal");

                if (_horizontalDirection < 0)
                {
                    _rigidbody2D.velocity = new Vector2(_horizontalDirection * _moveSpeed, _rigidbody2D.velocity.y);
                }
            }
            else
            {
                _horizontalDirection = Input.GetAxis("Horizontal");

                if (_horizontalDirection > 0)
                {
                    _rigidbody2D.velocity = new Vector2(_horizontalDirection * _moveSpeed, _rigidbody2D.velocity.y);
                }
            }
        }
    }

    private void findOutDirection()
    {
        if (_horizontalDirection < 0)
        {
            _isLookingRight = false;
            _characterTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_horizontalDirection > 0)
        {
            _isLookingRight = true;
            _characterTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isSliding = collision.gameObject.GetComponent<Ground>() || collision.gameObject.GetComponent<Wall>() ? true : false;
        if (collision.gameObject.GetComponent<Enemy>() || collision.gameObject.GetComponent<PressurePlate>()) _isSliding = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isSliding = collision.gameObject.GetComponent<Ground>() || collision.gameObject.GetComponent<Wall>() ? false : true;
        if (collision.gameObject.GetComponent<Enemy>() ||
            collision.gameObject.GetComponent<PressurePlate>() ||
            collision.gameObject.GetComponent<ExtraLive>()) _isSliding = false;
    }
}
